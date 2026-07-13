using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Represents the health of a character
    /// </summary>
    public class Health
    {
        public int HitPoints { get; set; } = 0;
        public int MaxHitPoints { get; set; } = 0;
        public string HitDice { get; set { field = value.Validated(); } } = string.Empty;
        public string MaxHitDice { get; set { field = value.Validated(); } } = string.Empty;
    }
}
