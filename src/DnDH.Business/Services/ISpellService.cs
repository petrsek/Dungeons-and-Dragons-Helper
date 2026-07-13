using System;
using System.Collections.Generic;
using System.Text;
using DnDH.Business.DTOs;

namespace DnDH.Business.Services
{
    /// <summary>
    /// Defines the contract for spell-related operations within the application.
    /// </summary>
    public interface ISpellService
    {
        /// <summary>
        /// Fetches all spells from the data source
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<SpellDTO> GetAll();
    }
}
