using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Zephyr.Define.Runtime
{
    public class DefineCenter
    {
        private static DefineCenter _instance;

        public static DefineCenter Instance()
        {
            if (_instance == null)
            {
                _instance = new DefineCenter();
                _instance.Init();
                _instance.LoadDefineFiles();
            }

            return _instance;
        }

        protected Dictionary<string, IDefine> _defines;

        public void Init()
        {
            _defines = new Dictionary<string, IDefine>();
        }

        [MenuItem("Zephyr/Refresh Defines")]
        public static void RefreshDefines()
        {
            var defineCount = Instance()._defines.Count;
            Instance().Init();
            Instance().LoadDefineFiles();
            var newDefineCount = Instance()._defines.Count;
            Debug.Log($"Defines Refreshed! Found {newDefineCount - defineCount} new defines");
        }

        public void LoadDefineFiles()
        {
            var defineObects = Resources.LoadAll<Define>("Defines");
            foreach (var define in defineObects)
            {
                _defines.Add(define.name, define);
            }
        }

        public IDefine GetDefine(FixedString64 defineName)
        {
            return GetDefine(defineName.ToString());
        }

        public IDefine GetDefine(string defineName)
        {
            if (!_defines.ContainsKey(defineName))
            {
                Debug.Log("No such Define! "+ defineName);
                return null;
            }

            return _defines[defineName];
        }

        public Dictionary<string, IDefine> GetDefines()
        {
            return _defines;
        }

        public IDefine[] GetDefinesOf<T>() where T : IComponent
        {
            var result = new List<IDefine>();
            
            foreach (var pair in _defines)
            {
                if (pair.Value.GetComponent<T>() != null)
                {
                    result.Add(pair.Value);
                }
            }

            return result.ToArray();
        }
    }
}