using Zephyr.Define.Runtime;

namespace Zephyr.Define.Test
{
    public static class DefineCenterExtensions
    {
        public static MockDefine AddDefine(this DefineCenter defineCenter, string name,
            IComponent[] components)
        {
            var newDefine = new MockDefine(name, components);
            defineCenter.GetDefines().Add(name, newDefine);
            return newDefine;
        }
    }
}