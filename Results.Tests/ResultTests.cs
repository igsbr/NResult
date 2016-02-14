using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Results.Tests
{
    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void SuccessShouldBeFalseIfAnyErrors()
        {
            var errors = new[] { "some error", "another error" };

            var actual = Result.Error(errors);

            Assert.IsFalse(actual.Success());
            Assert.IsTrue(actual.Errors.SequenceEqual(errors));
        }

        [TestMethod]
        public void ShouldReturnOkIfNoErrors()
        {
            var actual = Result.Error();

            Assert.IsTrue(actual.Success());
        }
        
        [TestMethod]
        public void AllCombinatorShouldReturnOkIfAllParameterAreOk()
        {
            var first = Result.Ok();
            var second = Result.Ok();
            var third = Result.Ok();

            var actual = Result.All(first, second, third);

            Assert.IsTrue(actual.Success());
        }

        [TestMethod]
        public void AllCombinatorShouldReturnErrorIfAnyParameterAreError()
        {
            var first = Result.Ok();
            var second = Result.Error("error message");
            var third = Result.Ok();

            var actual = Result.All(first, second, third);

            Assert.IsFalse(actual.Success());
            Assert.IsTrue(actual.Errors.SequenceEqual(new[] { "error message" }));
        }

        [TestClass]
        public class UsingResultsInMethodReturnTests
        {

            private Result ReturnsSimpleOk()
            {
                return Result.Ok();
            }

            private Result ReturnsSimpleError(string errorMessage)
            {
                return Result.Error(errorMessage);
            }

            private Result<int> ReturnsIntValue(int src)
            {
                return Result.Return(src);
            }

            private Result<int> ReturnsErrorWithIntType(string errorMessage)
            {
                return Result.Error(errorMessage);
            }

            [TestMethod]
            public void SimpleResultShouldReturnOk()
            {
                Assert.IsTrue(ReturnsSimpleOk().Success());
            }

            [TestMethod]
            public void SimpleResultShouldReturnError()
            {
                var actual = ReturnsSimpleError("some error");

                Assert.IsFalse(actual.Success());
                Assert.AreEqual("some error", actual.Errors[0]);
            }

            [TestMethod]
            public void ResultWithValueReturnsOk()
            {
                var actual = ReturnsIntValue(2);

                Assert.IsTrue(actual.Success());
                Assert.AreEqual(2, actual.Value);
            }

            [TestMethod]
            public void ShouldReturnResultWithValue()
            {
                var actual = ReturnsErrorWithIntType("another error");

                Assert.IsFalse(actual.Success());
                Assert.AreEqual("another error", actual.Errors[0]);
            }
        }
    }
}
