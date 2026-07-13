using DnDH.Business.UserHandling.PermisionStrategy;
using DnDH.Repo;

namespace DnDH.Test
{
    [TestClass]
    public class PlayerPermissionStrategyTests
    {
        [TestMethod]
        public void CanCreate_AreCorrect()
        {
            var strategy = new PlayerPermissionStrategy(1);
            Assert.IsFalse(strategy.CanCreateCampaigns);
            Assert.IsTrue(strategy.CanCreateCharacters);
        }

        [TestMethod]
        public void CanViewEditDeleteCharacters_OnlyOwned()
        {
            var strategy = new PlayerPermissionStrategy(1);
            var characters = new List<Character>
            {
                new Character("Char1", 1) { Id = 1, Owner = new User("User1") { Id = 1 } },
                new Character("Char2", 2) { Id = 2, Owner = new User("User2") { Id = 2 } }
            }.AsQueryable();

            Assert.AreEqual(1, strategy.CanView(characters).Count());
            Assert.AreEqual(1, strategy.CanViewDetail(characters).Count());
            Assert.AreEqual(1, strategy.CanEdit(characters).Count());
            Assert.AreEqual(1, strategy.CanDelete(characters).Count());
            Assert.AreEqual(1, strategy.CanView(characters).First().Id);
            Assert.AreEqual(1, strategy.CanViewDetail(characters).First().Id);
            Assert.AreEqual(1, strategy.CanEdit(characters).First().Id);
            Assert.AreEqual(1, strategy.CanDelete(characters).First().Id);
        }

        [TestMethod]
        public void CanViewCampaigns_OnlyParticipating()
        {
            var strategy = new PlayerPermissionStrategy(1);
            var campaigns = new List<Campaign>
            {
                new Campaign("C1", 1) { Id = 1, CharacterStatesInCampaigns = new List<CharacterStateInCampaign> { new CharacterStateInCampaign(1, 1) { Character = new Character("Char1", 1) { Owner = new User("User1") { Id = 1 } } } } },
                new Campaign("C2", 2) { Id = 2, CharacterStatesInCampaigns = new List<CharacterStateInCampaign> { new CharacterStateInCampaign(2, 2) { Character = new Character("Char2", 2) { Owner = new User("User2") { Id = 2 } } } } }
            }.AsQueryable();

            Assert.AreEqual(1, strategy.CanView(campaigns).Count());
            Assert.AreEqual(0, strategy.CanViewDetail(campaigns).Count());
            Assert.AreEqual(1, strategy.CanView(campaigns).First().Id);
        }

        [TestMethod]
        public void CanEditDeleteCampaigns_Never()
        {
            var strategy = new PlayerPermissionStrategy(1);
            var campaigns = new List<Campaign>
            {
                new Campaign("C1", 1) { Id = 1 },
                new Campaign("C2", 2) { Id = 2 }
            }.AsQueryable();

            Assert.AreEqual(0, strategy.CanEdit(campaigns).Count());
            Assert.AreEqual(0, strategy.CanDelete(campaigns).Count());
        }
    }
}