// tests/MyApp.Tests/Services/UserServiceTests.cs
using NUnit.Framework;
using Moq;
using Mocking.Services;
using Mocking.Repositories;
using Mocking.Models;

namespace Mocking.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            // Create mock repository
            _mockUserRepository = new Mock<IUserRepository>();
            
            // Create service with mocked dependency
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Test]
        public async Task GetUserByIdAsync_ValidId_ReturnsUser()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new User
            {
                Id = userId,
                Name = "John Doe",
                Email = "john@example.com",
                CreatedAt = DateTime.UtcNow
            };

            // Setup mock to return expected user
            _mockUserRepository
                .Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(result.Name, Is.EqualTo("John Doe"));
            Assert.That(result.Email, Is.EqualTo("john@example.com"));

            // Verify the repository method was called exactly once
            _mockUserRepository.Verify(x => x.GetByIdAsync(userId), Times.Once);
        }

        [Test]
        public async Task GetUserByIdAsync_UserNotFound_ReturnsNull()
        {
            // Arrange
            var userId = 999;
            _mockUserRepository
                .Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.That(result, Is.Null);
            _mockUserRepository.Verify(x => x.GetByIdAsync(userId), Times.Once);
        }

        [Test]
        public void GetUserByIdAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var invalidId = -1;

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(
                () => _userService.GetUserByIdAsync(invalidId));
            
            Assert.That(ex.ParamName, Is.EqualTo("id"));
            Assert.That(ex.Message, Contains.Substring("User ID must be positive"));

            // Verify repository was never called
            _mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public async Task CreateUserAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var name = "Jane Smith";
            var email = "jane@example.com";

            _mockUserRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<User>()); // No existing users

            _mockUserRepository
                .Setup(x => x.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(1); // Return valid ID

            // Act
            var result = await _userService.CreateUserAsync(name, email);

            // Assert
            Assert.That(result, Is.True);

            // Verify CreateAsync was called with correct user data
            _mockUserRepository.Verify(x => x.CreateAsync(It.Is<User>(u =>
                u.Name == name &&
                u.Email == email &&
                u.CreatedAt > DateTime.UtcNow.AddMinutes(-1)
            )), Times.Once);
        }

        [Test]
        public async Task CreateUserAsync_DuplicateEmail_ReturnsFalse()
        {
            // Arrange
            var name = "Jane Smith";
            var email = "existing@example.com";

            var existingUsers = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "existing@example.com" }
            };

            _mockUserRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(existingUsers);

            // Act
            var result = await _userService.CreateUserAsync(name, email);

            // Assert
            Assert.That(result, Is.False);

            // Verify CreateAsync was never called
            _mockUserRepository.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        [TestCase("")]
        [TestCase("   ")]
        public void CreateUserAsync_InvalidName_ThrowsArgumentException(string invalidName)
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(
                () => _userService.CreateUserAsync(invalidName, "test@example.com"));
            
            Assert.That(ex.ParamName, Is.EqualTo("name"));
        }

        [Test]
        public void CreateUserAsync_NullName_ThrowsArgumentException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(
                () => _userService.CreateUserAsync(null, "test@example.com"));
            
            Assert.That(ex.ParamName, Is.EqualTo("name"));
        }

        [Test]
        [TestCase("invalid-email")]
        [TestCase("@example.com")]
        [TestCase("test@")]
        [TestCase("")]
        public void CreateUserAsync_InvalidEmail_ThrowsArgumentException(string invalidEmail)
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(
                () => _userService.CreateUserAsync("Valid Name", invalidEmail));
            
            Assert.That(ex.ParamName, Is.EqualTo("email"));
        }

        [Test]
        public async Task UpdateUserEmailAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var userId = 1;
            var newEmail = "newemail@example.com";
            var existingUser = new User
            {
                Id = userId,
                Name = "John Doe",
                Email = "old@example.com",
                CreatedAt = DateTime.UtcNow
            };

            _mockUserRepository
                .Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(existingUser);

            _mockUserRepository
                .Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.UpdateUserEmailAsync(userId, newEmail);

            // Assert
            Assert.That(result, Is.True);

            // Verify the user's email was updated
            _mockUserRepository.Verify(x => x.UpdateAsync(It.Is<User>(u => 
                u.Id == userId && u.Email == newEmail)), Times.Once);
        }

        [Test]
        public async Task UpdateUserEmailAsync_UserNotFound_ReturnsFalse()
        {
            // Arrange
            var userId = 999;
            var newEmail = "newemail@example.com";

            _mockUserRepository
                .Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _userService.UpdateUserEmailAsync(userId, newEmail);

            // Assert
            Assert.That(result, Is.False);

            // Verify UpdateAsync was never called
            _mockUserRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [TearDown]
        public void TearDown()
        {
            // Verify all setups were used (optional)
            _mockUserRepository.VerifyAll();
        }
    }
}