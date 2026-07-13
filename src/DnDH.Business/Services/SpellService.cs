using DnDH.Business.DTOs;
using DnDH.Business.UserHandling;
using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.Services
{
    /// <summary>
    /// A concrete implementation of ISpellService using Entity Framework Core.
    /// </summary>
    public class SpellService : AbstractService, ISpellService
    {
        public SpellService(IUserContext userContext, AppDbContext dbContext) : base(userContext, dbContext)
        {
        }

        public IReadOnlyList<SpellDTO> GetAll()
        {
           return _dbContext.Spells.Select(s => s.MapToDTO()).ToList();
        }
    }
}
