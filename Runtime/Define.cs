using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zephyr.Define.Runtime
{
    
    [CreateAssetMenu(menuName = "Define")]
    public class Define : SerializedScriptableObject, IDefine
    {
        [ReadOnly]
        public string Name;
        
        [ValidateInput("IsComponentsCompatible","组件兼容性有问题，参见输出")]
        [OnValueChanged("OnComponentsChanged", true)]
        public IComponent[] Components = new IComponent[]{};
        
        public string GetName()
        {
            return Name;
        }
        
        /// <summary>
        /// 获取指定类型的组件，注意如果有重复，只会返回第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : IComponent
        {
            foreach (var component in Components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }

            return default(T);
        }

        public IComponent[] GetComponents()
        {
            return Components;
        }

#if UNITY_EDITOR

        public T AddComponent<T>() where T : IComponent, new()
        {
            var listComponents = new List<IComponent>();
            listComponents.AddRange(Components);
            var newComponent = new T();
            listComponents.Add(newComponent);
            Components = listComponents.ToArray();
            return newComponent;
        }

        private void OnComponentsChanged()
        {
            foreach (var component in Components)
            {
                component.Init(this);
            }
        }
        
        /// <summary>
        /// 在这里做好各种组件兼容性检查，比如『建设UI组件』必须依赖『建设组件』
        /// </summary>
        /// <param name="components"></param>
        /// <returns></returns>
        private bool IsComponentsCompatible(IComponent[] components)
        {
            if (components == null)
            {
                return true;
            }

            foreach (var component in components)
            {
                if (!component.CheckCompatible(components))
                {
                    return false;
                }
            }
            
            //检查重复
            //理论上这里只需要检查新添加的（最后一个）是否跟之前有重复即可
            for (int i = 0; i < components.Length - 1; i++)
            {
                if (components[i].IsUnique() &&
                    components[i].GetType() == components[components.Length - 1].GetType())
                {
                    Debug.Log("组件必须唯一");
                    return false;
                }
            }
            
            return true;
        }
        
        protected override void OnAfterDeserialize()
        {
            base.OnAfterDeserialize();
            foreach (var component in Components)
            {
                component?.Init(this);
            }
        }

        protected override void OnBeforeSerialize()
        {
            base.OnBeforeSerialize();
            Name = name;
        }
#endif
    }
}