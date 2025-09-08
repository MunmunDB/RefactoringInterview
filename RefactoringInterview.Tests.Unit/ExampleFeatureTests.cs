using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RefactoringInterview.Tests.Unit
{
    [TestClass]
    public class ExampleFeatureTests
    {
        [TestMethod]
        public void ExampleAction_Always_ReturnsTrue()
        {
            // Arrange
            var sut = new ExampleFeature();

            // Act
            var result = sut.ExampleAction();

            // Assert
            Assert.IsTrue(result);
        }
    }
}
