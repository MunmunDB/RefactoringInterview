using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactoringInterview.Core.Application;
using System.Collections.Generic;

namespace RefactoringInterview.Tests.Unit
{
    [TestClass]
    public class PasswordManagerTests
    {
        [TestMethod]
        [DataRow("password123", "password123","")]
        [DataRow("Password!@#", "Password!@#","")]
        public void Compare_Always_ReturnsTrue(string password, string retypepassword,string errMsg)
        {
            // Arrange
            var sut = new PasswordManager();

            // Act
            var result = sut.Compare(password, retypepassword, out errMsg);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("password123")]
        public void Encrypt_Always_ReturnsEncryptedString(string password)
        {
            // Arrange
            var sut = new PasswordManager();

            // Act
            var encryptResult = sut.Encrypt(password);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(encryptResult));
            Assert.IsTrue(encryptResult.Contains(":")); // Check if the result contains the salt and hash separator
        }

        [TestMethod]
        [DataRow("short", "")]
        [DataRow("1234567", "")]
        public void Validate_PasswordTooShort_ReturnsFalse(string password, string errMsg)
        {
            // Arrange
            var sut = new PasswordManager();
            // Act
            var result = sut.Validate(password, out errMsg);
            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Password must be at least 8 characters long.", errMsg);
        }

        [TestMethod]
        [DataRow("longenough","")]
        [DataRow("Password123!","")]
        public void Validate_PasswordMeetsCriteria_ReturnsTrue(string password, string errMsg)
        {
            // Arrange
            var sut = new PasswordManager();
            // Act
            var result = sut.Validate(password, out errMsg);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("password123", "differentPassword","")]
        public void Compare_PasswordsDoNotMatch_ReturnsFalse(string password, string retypepassword, string errMsg)
        {
            // Arrange
            var sut = new PasswordManager();
            // Act
            var result = sut.Compare(password, retypepassword, out errMsg);
            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        [DataRow("Password123!")]
        public void Encrypt_SamePasswordMultipleTimes_ReturnsDifferentHashes(string password)
        {
            // Arrange
            var sut = new PasswordManager();
            // Act
            var hash1 = sut.Encrypt(password);
            var hash2 = sut.Encrypt(password);
            // Assert
            Assert.AreNotEqual(hash1, hash2); // Ensure that the hashes are different due to unique salts
        }
        [TestMethod]
        [DataRow("","")]
        [DataRow(null,"")]
        public void Validate_EmptyOrNullPassword_ReturnsFalse(string password, string errMsg)
        {
            // Arrange
            var sut = new PasswordManager();
            // Act
            var result = sut.Validate(password, out errMsg);
            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        [DataRow("        ","")] // 8 spaces
        public void Validate_PasswordWithOnlySpaces_ReturnsTrue(string password,string errMsg)
        {
            // Arrange
            var sut = new PasswordManager();
            // Act
            var result = sut.Validate(password, out errMsg);
            // Assert
            Assert.IsTrue(result); // Assuming spaces are counted as valid characters
        }
        [TestMethod]
        [DataRow("pässwörd123", "pässwörd123","")] // Unicode characters
        public void Compare_UnicodePasswords_ReturnsTrue(string password, string retypepassword, string errMsg)
        {
            // Arrange
            var sut = new PasswordManager();
            // Act
            var result = sut.Compare(password, retypepassword, out errMsg);
            // Assert
            Assert.IsTrue(result);
        }
    }
}
