using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DnDH.Repo
{
    /// <summary>
    /// Represents a set of abilities for a character
    /// </summary>
    public class Abilities
    {
        public int Strength { get; set; } = 0;
        public int Dexterity { get; set; } = 0;
        public int Constitution { get; set; } = 0;
        public int Intelligence { get; set; } = 0;
        public int Wisdom { get; set; } = 0;
        public int Charisma { get; set; } = 0;

        public int StrengthModifier => CalculateModifier(Strength);
        public int DexterityModifier => CalculateModifier(Dexterity);
        public int ConstitutionModifier => CalculateModifier(Constitution);
        public int IntelligenceModifier => CalculateModifier(Intelligence);
        public int WisdomModifier => CalculateModifier(Wisdom);
        public int CharismaModifier => CalculateModifier(Charisma);

        /// <summary>
        /// Calculates the ability modifier based on the ability score using the standard D&D 5e formula: (score - 10) / 2
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        private static int CalculateModifier(int score) => (int)Math.Floor((score - 10) / 2.0);
    }
}
