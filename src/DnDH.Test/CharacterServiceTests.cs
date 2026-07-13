using DnDH.Business.DTOs;
using DnDH.Business.Services;
using DnDH.Business.UserHandling;
using DnDH.Business.UserHandling.PermisionStrategy;
using DnDH.Repo;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDH.Test
{
    [TestClass]
    public class CharacterServiceTests
    {
        private AppDbContext _dbContext = null!;
        private Mock<IUserContext> _userContextMock = null!;
        private CharacterService _service = null!;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(options);
            _userContextMock = new Mock<IUserContext>();

            _service = new CharacterService(_userContextMock.Object, _dbContext);
        }

        private void SetupUserSession(int userId, UserRole role)
        {
            var strategy = PermissionStrategyFactory.CreatePermissionStrategy(userId,role);
            _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
            _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(userId, $"User{userId}", strategy));
        }

        [TestMethod]
        public void Add_UserWithPermission_CreatesCharacter()
        {
            // Arrange
            SetupUserSession(1, UserRole.Player);

            // Act
            var result = _service.Add("Gandalf");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Gandalf", result.Name);
            Assert.AreEqual(1, result.OwnerId);
            Assert.AreEqual(1, _dbContext.Characters.Count());
        }

        [TestMethod]
        public void GetDetail_UserHasPermission_ReturnsCharacter()
        {
            // Arrange
            SetupUserSession(1, UserRole.Player);
            var user = new User("User1", UserRole.Player) { Id = 1 };
            _dbContext.Users.Add(user);
            var character = new Character("Legolas", 1) { Owner = user };
            _dbContext.Characters.Add(character);
            _dbContext.SaveChanges();

            // Act
            var result = _service.GetDetail(character.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Legolas", result.Name);
        }

        [TestMethod]
        public void UpdateBase_UserHasPermission_UpdatesCharacterInfo()
        {
            // Arrange
            SetupUserSession(1, UserRole.Player);
            var user = new User("User1", UserRole.Player) { Id = 1 };
            _dbContext.Users.Add(user);
            var character = new Character("Frodo", 1) { Owner = user };
            _dbContext.Characters.Add(character);
            _dbContext.SaveChanges();

            // Act
            var result = _service.UpdateBase(character.Id, name: "Frodo Baggins", level: 5);

            // Assert
            Assert.AreEqual("Frodo Baggins", result.Name);
            Assert.AreEqual(5, result.Level);

            var dbChar = _dbContext.Characters.First();
            Assert.AreEqual("Frodo Baggins", dbChar.Name);
            Assert.AreEqual(5, dbChar.Level);
        }

        [TestMethod]
        public void Delete_UserHasPermission_DeletesCharacter()
        {
            // Arrange
            SetupUserSession(1, UserRole.Player);
            var user = new User("User1", UserRole.Player) { Id = 1 };
            _dbContext.Users.Add(user);
            var character = new Character("Gimli", 1) { Owner = user };
            _dbContext.Characters.Add(character);
            _dbContext.SaveChanges();

            // Act
            _service.Delete(character.Id);

            // Assert
            Assert.AreEqual(0, _dbContext.Characters.Count());
        }

        [TestMethod]
        public void AddSpellcasting_CharacterExists_AddsSpellcasting()
        {
            // Arrange
            SetupUserSession(1, UserRole.Player);
            var user = new User("User1", UserRole.Player) { Id = 1 };
            _dbContext.Users.Add(user);
            var character = new Character("Saruman", 1) { Owner = user };
            _dbContext.Characters.Add(character);
            _dbContext.SaveChanges();

            // Act
            _service.AddSpellcasting(character.Id);

            // Assert
            var dbChar = _dbContext.Characters.Include(c => c.Spellcasting).First(c => c.Id == character.Id);
            Assert.IsNotNull(dbChar.Spellcasting);
        }
    }
}
