#if UNITY_EDITOR
using Sirenix.OdinInspector;

#endif

namespace Zephyr.Define.Runtime
{
    public class DefineInfo
    {
#if UNITY_EDITOR
        [ReadOnly]
#endif
        public string DefineName;
        
#if UNITY_EDITOR
        [AssetList(CustomFilterMethod = "CustomFilter")]
        [OnValueChanged("UpdateDefineName")]
        public Define Define;

        private void UpdateDefineName()
        {
            DefineName = Define.GetName();
        }
        
        protected virtual bool CustomFilter(Define define)
        {
            return true;
        }
#endif
    }
    
}