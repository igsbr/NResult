using Microsoft.VisualStudio.TestTools.UnitTesting;
using Results.Linq;
using System.Linq;

namespace Results.Tests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void OkResultAfterErrorInLinqExpression()
        {
            Result<int> errorResult = Result.Error("error message");
            Result<int> okResult = Result.Return(100);

            var actual = from x in errorResult
                         from i in okResult
                         select i * x;

            Assert.IsFalse(actual.Success());
            Assert.IsTrue(actual.Errors.SequenceEqual(new[] { "error message" }));
        }

        [TestMethod]
        public void ErrorAfterOkResultInLinqExpression()
        {
            Result<int> okResult = Result.Return(100);
            Result<int> errorResult = Result.Error("error message");

            var actual = from i in okResult
                         from x in errorResult
                         select i * x;

            Assert.IsFalse(actual.Success());
            Assert.IsTrue(actual.Errors.SequenceEqual(new[] { "error message" }));
        }

        [TestMethod]
        public void ErrorsOnlyInLinqExpression()
        {
            Result<int> first = Result.Error("first message");
            Result<int> second = Result.Error("second message");

            var actual = from x in first
                         from y in second
                         select x * y;

            Assert.IsFalse(actual.Success());
            Assert.IsTrue(actual.Errors.SequenceEqual(new[] { "first message", "second message" }));
        }

        [TestMethod]
        public void NegativeResultAndDifferentTypesInSimpleLinqExpression()
        {
            var actual = from x in Result.Error("error message")
                         let xStr = x.ToString()
                         select "<" + xStr + ">";

            Assert.IsFalse(actual.Success());
            Assert.IsTrue(actual.Errors.SequenceEqual(new[] { "error message" }));
        }

        [TestMethod]
        public void CouplePositiveResultsAndDifferentTypesInLinqExpression()
        {
            var actual = from x in Result.Return(10)
                         from y in Result.Return("3")
                         let z = int.Parse(y)
                         select x * z;

            Assert.IsTrue(actual.Success());
            Assert.AreEqual(30, actual.Value);
        }

        [TestMethod]
        public void PositiveResultAndDifferentTypesInSimpleLinqExpression()
        {
            var actual = from x in Result.Return(11)
                         let xStr = x.ToString()
                         select "<" + xStr + ">";

            Assert.IsTrue(actual.Success());
            Assert.AreEqual("<11>", actual.Value);
        }
    }
}
