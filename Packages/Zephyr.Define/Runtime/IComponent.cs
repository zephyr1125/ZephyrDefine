using Unity.Entities;
using UnityEngine;

namespace Zephyr.Define.Runtime
{
    public interface IComponent
    {
        /// <summary>
        /// 在Define构建entity之前，各个组件提供ComponentType以便创建Archetype,避免频繁搬运
        /// </summary>
        /// <returns></returns>
        ComponentType[] GetECSComponentTypes();
        
        /// <summary>
        /// 基于本component进行ECS的component构建的方法,必须实现
        /// </summary>
        /// <param name="entity"></param>
        void SetECSComponent(EntityManager entityManager, Entity entity);

        void SetECSComponentConverting(GameObjectConversionSystem conversionSystem,
            Component conventionComponent, EntityManager entityManager, Entity entity);
        
#if UNITY_EDITOR
        void Init(Define parentDefine);
        
        bool IsUnique();
        
        bool CheckCompatible(IComponent[] components);
#endif
    }
}