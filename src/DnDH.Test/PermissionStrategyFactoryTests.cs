using DnDH.Business.UserHandling.PermisionStrategy;
using DnDH.Repo;
using System;

namespace DnDH.Test
{
    [TestClass]
    public class PermissionStrategyFactoryTests
    {
        [TestMethod]
        public void CreatePermissionStrategy_DungeonMasterRole_ReturnsDMPermissionStrategy()
        {
            // Act
            var strategy = PermissionStrategyFactory.CreatePermissionStrategy(1, UserRole.DungeonMaster);

            // Assert
            Assert.IsInstanceOfType(strategy, typeof(DMPermissionStrategy));
        }

        [TestMethod]
        public void CreatePermissionStrategy_PlayerRole_ReturnsPlayerPermissionStrategy()
        {
            // Act
            var strategy = PermissionStrategyFactory.CreatePermissionStrategy(2, UserRole.Player);

            // Assert
            Assert.IsInstanceOfType(strategy, typeof(PlayerPermissionStrategy));
        }

        [TestMethod]
        public void CreatePermissionStrategy_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => PermissionStrategyFactory.CreatePermissionStrategy(0, UserRole.Player));
            Assert.Throws<ArgumentOutOfRangeException>(() => PermissionStrategyFactory.CreatePermissionStrategy(-1, UserRole.DungeonMaster));
        }

        [TestMethod]
        public void CreatePermissionStrategy_InvalidRole_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => PermissionStrategyFactory.CreatePermissionStrategy(1, (UserRole)999));
        }
    }
}