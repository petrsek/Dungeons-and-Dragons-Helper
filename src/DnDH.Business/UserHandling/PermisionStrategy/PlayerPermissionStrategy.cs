using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.UserHandling.PermisionStrategy
{
    /// <summary>
    /// Permisions of the player role
    /// </summary>
    public class PlayerPermissionStrategy : IPermissionStrategy
    {
        private readonly int _userId;
        public bool CanCreateCampaigns => false;

        public bool CanCreateCharacters => true;

        /// <summary>
        /// Initializes a new instance of the PlayerPermissionStrategy class with the provided username
        /// </summary>
        /// <param name="username"></param>
        public PlayerPermissionStrategy(int userId)
        {
            _userId = userId;
        }
        public IQueryable<Character> CanView(IQueryable<Character> characters)
        {
            return characters.Where(c => c.Owner.Id == _userId);
        }

        public IQueryable<Character> CanEdit(IQueryable<Character> characters)
        {
            return characters.Where(c => c.Owner.Id == _userId);
        }

        public IQueryable<Character> CanDelete(IQueryable<Character> characters)
        {
            return characters.Where(c => c.Owner.Id == _userId);
        }

        public IQueryable<Campaign> CanView(IQueryable<Campaign> campaigns)
        {
            return campaigns.Where(c => c.CharacterStatesInCampaigns.Any(cs => cs.Character.Owner.Id == _userId));
        }

        public IQueryable<Campaign> CanEdit(IQueryable<Campaign> campaigns)
        {
            return Enumerable.Empty<Campaign>().AsQueryable();
        }

        public IQueryable<Campaign> CanDelete(IQueryable<Campaign> campaigns)
        {
            return Enumerable.Empty<Campaign>().AsQueryable();
        }

        public IQueryable<Character> CanViewDetail(IQueryable<Character> characters)
        {
            return characters.Where(c => c.Owner.Id == _userId);
        }

        public IQueryable<Campaign> CanViewDetail(IQueryable<Campaign> campaigns)
        {
            return Enumerable.Empty<Campaign>().AsQueryable();
        }
    }
}
