using DnDH.Business.UserHandling.PermisionStrategy;
using DnDH.Repo;
using System.Collections.Generic;
using System.Linq;

namespace DnDH.Test
{
    [TestClass]
    public class DMPermissionStrategyTests
    {
        [TestMethod]
        public void CanCreate_True()
        {
            var strategy = new DMPermissionStrategy(1);
            Assert.IsTrue(strategy.CanCreateCampaigns);
            Assert.IsTrue(strategy.CanCreateCharacters);
        }

        [TestMethod]
        public void CanViewEditCharacters_All()
        {
            var strategy = new DMPermissionStrategy(1);
            var characters = new List<Character>
            {
                new Character("Char1", 1) { Id = 1, Owner = new User("User1") { Id = 1 } },
                new Character("Char2", 2) { Id = 2, Owner = new User("User2") { Id = 2 } }
            }.AsQueryable();

            Assert.AreEqual(2, strategy.CanView(characters).Count());
            Assert.AreEqual(2, strategy.CanViewDetail(characters).Count());
            Assert.AreEqual(2, strategy.CanEdit(characters).Count());
        }

        [TestMethod]
        public void CanDeleteCharacters_OnlyOwned()
        {
            var strategy = new DMPermissionStrategy(1);
            var characters = new List<Character>
            {
                new Character("Char1", 1) { Id = 1, Owner = new User("User1") { Id = 1 } },
                new Character("Char2", 2) { Id = 2, Owner = new User("User2") { Id = 2 } }
            }.AsQueryable();

            Assert.AreEqual(1, strategy.CanDelete(characters).Count());
            Assert.AreEqual(1, strategy.CanDelete(characters).First().Id);
        }

        [TestMethod]
        public void CanViewEditDeleteCampaigns_OnlyAuthored()
        {
            var strategy = new DMPermissionStrategy(1);
            var campaigns = new List<Campaign>
            {
                new Campaign("Camp1", 1) { Id = 1 },
                new Campaign("Camp2", 2) { Id = 2 }
            }.AsQueryable();

            Assert.AreEqual(1, strategy.CanView(campaigns).Count());
            Assert.AreEqual(1, strategy.CanViewDetail(campaigns).Count());
            Assert.AreEqual(1, strategy.CanEdit(campaigns).Count());
            Assert.AreEqual(1, strategy.CanDelete(campaigns).Count());
            Assert.AreEqual(1, strategy.CanView(campaigns).First().Id);
            Assert.AreEqual(1, strategy.CanViewDetail(campaigns).First().Id);
            Assert.AreEqual(1, strategy.CanEdit(campaigns).First().Id);
            Assert.AreEqual(1, strategy.CanDelete(campaigns).First().Id);
        }
    }
}