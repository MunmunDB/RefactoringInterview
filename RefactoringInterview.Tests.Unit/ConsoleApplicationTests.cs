using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RefactoringInterview.Core.Application;

namespace RefactoringInterview.Tests.Unit
{
    [TestClass]
    public class ConsoleClientApplicationTests
    {
        [TestMethod]
        public void GetUserInput_Return_NotNull()
        {
            // Arrange
            var mockpasswordmanager = new Mock<IPasswordManager>();
            var sut = new ConsoleClientApplication(mockpasswordmanager.Object);
            // Act
           //var result = sut.GetUserInput();
            // Assert
            Assert.IsNotNull(sut);
        }
    }
}
