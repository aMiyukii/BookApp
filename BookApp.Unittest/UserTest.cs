using BookApp.Core.Services;
using BookApp.Unittest.Fake_Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            var fakeRepository = new FakeUserRepository();
            _userService = new UserService(fakeRepository);
        }

        [TestMethod]
        public async Task LoginAsync_ValidCredentials_ShouldReturnTrue()
        {
            // Arrange
            string validEmail = "test@example.com";
            string validPassword = "password";

            // Act
            bool result = await _userService.LoginAsync(validEmail, validPassword);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task LoginAsync_InvalidCredentials_ShouldReturnFalse()
        {
            // Arrange
            string invalidEmail = "invalid@example.com";
            string invalidPassword = "invalidpassword";

            // Act
            bool result = await _userService.LoginAsync(invalidEmail, invalidPassword);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetUserIdAsync_ExistingEmail_ShouldReturnUserId()
        {
            // Arrange
            string existingEmail = "test@example.com";
            int expectedUserId = 1;

            // Act
            int userId = await _userService.GetUserIdAsync(existingEmail);

            // Assert
            Assert.AreEqual(expectedUserId, userId);
        }

        [TestMethod]
        public async Task GetUserIdAsync_NonExistingEmail_ShouldReturnZero()
        {
            // Arrange
            string nonExistingEmail = "nonexisting@example.com";

            // Act
            int userId = await _userService.GetUserIdAsync(nonExistingEmail);

            // Assert
            Assert.AreEqual(0, userId);
        }
    }
}
