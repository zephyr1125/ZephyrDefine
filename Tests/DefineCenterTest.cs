using NUnit.Framework;
using Zephyr.Define.Runtime;
using Zephyr.Define.Test.Utils;

namespace Zephyr.Define.Test
{
    public class DefineCenterTest
    {
        private class MockComponentA : ComponentBase{}
        private class MockComponentB : ComponentBase{}

        [TearDown]
        public void TearDown()
        {
            DefineCenter.Instance().GetDefines().Clear();
        }
        
        [Test]
        public void GetDefinesOf_Correct()
        {
            DefineCenter.Instance().AddDefine("a", new IComponent[] {new MockComponentA()});
            DefineCenter.Instance().AddDefine("b", new IComponent[] {new MockComponentA()});
            DefineCenter.Instance().AddDefine("c", new IComponent[] {new MockComponentB()});

            var result = DefineCenter.Instance().GetDefinesOf<MockComponentA>();
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("a", result[0].GetName());
            Assert.AreEqual("b", result[1].GetName());
        }
    }
}