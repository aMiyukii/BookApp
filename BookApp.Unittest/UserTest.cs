using BookApp.Core.Services;
using BookApp.Unittest.Fake_Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using BookApp.Core.DTO;

namespace UnitTests
{
    [TestClass]
    public class UserTest
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
            string validEmail = "test1@example.com";
            string validPassword = "password1";

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
        public async Task LoginAsync_NullEmail_ShouldThrowArgumentNullException()
        {
            // Arrange
            string invalidEmail = null;
            string password = "password";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                _userService.LoginAsync(invalidEmail, password));
        }

        [TestMethod]
        public async Task LoginAsync_NullPassword_ShouldThrowArgumentNullException()
        {
            // Arrange
            string email = "test@example.com";
            string invalidPassword = null;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                _userService.LoginAsync(email, invalidPassword));
        }

        [TestMethod]
        public async Task GetUserIdAsync_ExistingEmail_ShouldReturnUserId()
        {
            // Arrange
            string existingEmail = "test1@example.com";
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

        [TestMethod]
        public async Task GetUserIdAsync_NullEmail_ShouldThrowArgumentNullException()
        {
            // Arrange
            string invalidEmail = null;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                _userService.GetUserIdAsync(invalidEmail));
        }

        [TestMethod]
        public async Task CreateUserAsync_ValidUser_ShouldCreateUser()
        {
            // Arrange
            string name = "Test User";
            string email = "newuser@example.com";
            string password = "newpassword";

            // Act
            bool result = await _userService.CreateUserAsync(name, email, password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreateUserAsync_ExistingEmail_ShouldThrowException()
        {
            // Arrange
            string existingEmail = "test1@example.com";
            string name = "Test User";
            string password = "newpassword";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                _userService.CreateUserAsync(name, existingEmail, password));
        }

        [TestMethod]
        public async Task CreateUserAsync_MissingFields_ShouldThrowArgumentException()
        {
            // Arrange
            string name = "";
            string email = "newuser@example.com";
            string password = "newpassword";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _userService.CreateUserAsync(name, email, password));
        }

        [TestMethod]
        public async Task UserExistsAsync_ExistingEmail_ShouldReturnTrue()
        {
            // Arrange
            string existingEmail = "test1@example.com";

            // Act
            bool result = await _userService.UserExistsAsync(existingEmail);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UserExistsAsync_NonExistingEmail_ShouldReturnFalse()
        {
            // Arrange
            string nonExistingEmail = "nonexisting@example.com";

            // Act
            bool result = await _userService.UserExistsAsync(nonExistingEmail);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UserExistsAsync_NullEmail_ShouldThrowArgumentNullException()
        {
            // Arrange
            string invalidEmail = null;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                _userService.UserExistsAsync(invalidEmail));
        }
        
        [TestClass]
        public class UserDTOTest
        {
            [TestMethod]
            public void UserDTO_Constructor_ValidInitialization()
            {
                // Arrange & Act
                var userDto = new UserDTO(1, "test@example.com", "password");

                // Assert
                Assert.AreEqual(1, userDto.Id);
                Assert.AreEqual("test@example.com", userDto.Emailaddress);
                Assert.AreEqual("password", userDto.Password);
            }

        }
    }
}
