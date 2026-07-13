using DnDH.Repo;

namespace DnDH.Test
{
    [TestClass]
    public class AbilitiesTests
    {
        [TestMethod]
        [DataRow(10, 0)]
        [DataRow(12, 1)]
        [DataRow(13, 1)]
        [DataRow(14, 2)]
        [DataRow(8, -1)]
        [DataRow(9, -1)]
        [DataRow(1, -5)]
        [DataRow(20, 5)]
        [DataRow(30, 10)]
        public void AbilityModifiers_CalculatedCorrectly(int score, int expectedModifier)
        {
            var abilities = new Abilities
            {
                Strength = score,
                Dexterity = score,
                Constitution = score,
                Intelligence = score,
                Wisdom = score,
                Charisma = score
            };

            Assert.AreEqual(expectedModifier, abilities.StrengthModifier);
            Assert.AreEqual(expectedModifier, abilities.DexterityModifier);
            Assert.AreEqual(expectedModifier, abilities.ConstitutionModifier);
            Assert.AreEqual(expectedModifier, abilities.IntelligenceModifier);
            Assert.AreEqual(expectedModifier, abilities.WisdomModifier);
            Assert.AreEqual(expectedModifier, abilities.CharismaModifier);
        }
    }
}