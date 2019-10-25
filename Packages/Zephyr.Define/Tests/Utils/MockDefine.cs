using Zephyr.Define.Runtime;

namespace Zephyr.Define.Test.Utils
{
    public class MockDefine : IDefine
    {
        private string _name;
        private IComponent[] _components;

        public MockDefine(string name, IComponent[] components)
        {
            _name = name;
            _components = components;
        }

        public string GetName()
        {
            return _name;
        }

        public T GetComponent<T>() where T : IComponent
        {
            foreach (var component in _components)
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
            return _components;
        }
    }
}