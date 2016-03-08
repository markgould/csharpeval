using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionEvaluator.Tests
{
    [TestClass]
    public class InheritanceTests
    {
        public class ScopeCompileTest<T> where T : IBaseInterface
        {
            public bool Test(T scope, string expression)
            {
                var expr = new CompiledExpression<bool>(expression);
                var func = expr.ScopeCompile<T>();

                return func.Invoke(scope);
            }
        }

        public interface IBaseInterface
        {
            int SomeProperty { get; set; }
        }

        public interface ISuperInterface : IBaseInterface
        {
            int SomeProperty2 { get; set; }
        }

        public class ImplementingClass : ISuperInterface
        {
            public int SomeProperty { get; set; }
            public int SomeProperty2 { get; set; }
        }

        [TestMethod]
        public void ShouldReturnTrueForBaseProperty()
        {
            var scope = new ImplementingClass
            {
                SomeProperty = 1
            };

            var str = "SomeProperty > 0";
            var test = new ScopeCompileTest<ISuperInterface>();

            Assert.IsTrue(test.Test(scope, str));
        }

        [TestMethod]
        public void ShouldReturnTrueForSuperProperty()
        {
            var scope = new ImplementingClass
            {
                SomeProperty2 = 2
            };

            var str = "SomeProperty2 > 0";
            var test = new ScopeCompileTest<ISuperInterface>();

            Assert.IsTrue(test.Test(scope, str));
        }
    }


}
