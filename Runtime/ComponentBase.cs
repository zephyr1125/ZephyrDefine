using Unity.Entities;
using UnityEngine;

namespace Zephyr.Define.Runtime
{
    public class ComponentBase : IComponent
    {
        public virtual ComponentType[] GetECSComponentTypes()
        {
            //默认不创建ECS组件
            return new ComponentType[] { };
        }

        public virtual void SetECSComponent(EntityManager entityManager, Entity entity, IDefine define)
        {
            //默认不创建ECS组件
        }
        
        public virtual void SetECSComponentConverting(GameObjectConversionSystem conversionSystem,
            Component conventionComponent, EntityManager entityManager, Entity entity, IDefine define)
        {
            //默认与SetECSComponent一致
            SetECSComponent(entityManager, entity, define);
        }
        
#if UNITY_EDITOR
        protected Define _define;

        /// <summary>
        /// 组件是否要求唯一性，默认为true
        /// </summary>
        public virtual bool IsUnique()
        {
            return true;
        }

        public virtual void Init(Define parentDefine)
        {
            _define = parentDefine;
        }

        /// <summary>
        /// 如果有需要,可以检查兼容性
        /// </summary>
        /// <param name="components"></param>
        public virtual bool CheckCompatible(IComponent[] components)
        {
            return true;
        }
#endif
    }
}