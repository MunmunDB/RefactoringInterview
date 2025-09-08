using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RefactoringInterview.Core.Application;
using RefactoringInterview.Core.Domain;
using System;

namespace RefactoringInterview.Tests.Unit
{
    [TestClass]
    public class SecurityManagerTests
    {      

        [TestMethod]
        public void CreateUser_ValidUser_ShouldCallAddUser()
        {
            // Arrange
            var mockInputOutput = new Mock<IClientApplication>();
            var mockUserRepository = new Mock<IUserRepository>();
            var user = new User() { FullName = "John Doe", Username = "johndoe", EncryptedPassword = "password123" };
            mockInputOutput.Setup(io => io.GetUserInput()).Returns(user);
            var sut = new SecurityManager(mockInputOutput.Object, mockUserRepository.Object);
            // Act
            sut.CreateUser();
            // Assert
            mockUserRepository.Verify(repo => repo.AddUser(It.Is<User>(u => u.Username == user.Username)), Times.Once);
            mockInputOutput.Verify(io => io.Response(It.Is<string>(msg => msg.Contains("created successfully"))), Times.Once);
        }
        [TestMethod]
        [DataRow(default(User))]
        public void CreateUser_NullUser_ShouldNotCallAddUser(User user)
        {
            // Arrange
            var mockInputOutput = new Mock<IClientApplication>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockInputOutput.Setup(io => io.GetUserInput()).Returns(user);
            var sut = new SecurityManager(mockInputOutput.Object, mockUserRepository.Object);
            // Act
            sut.CreateUser();
            // Assert
            mockUserRepository.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Never);
            mockInputOutput.Verify(io => io.Response(It.Is<string>(msg => msg.Contains("creation failed"))), Times.Once);
        }
       
    }
}
