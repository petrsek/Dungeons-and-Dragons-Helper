using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Represents a player's inventory
    /// </summary>
    public class Inventory
    {
        public int Gold { get; set; } = 0;
        public List<string> EquippedItems {  get; set; } = [];
        public List<string> OtherItems { get; set; } = [];
    }
}
