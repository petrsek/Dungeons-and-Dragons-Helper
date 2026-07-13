using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.UserHandling.PermisionStrategy
{
    /// <summary>
    /// Permission strategy of Dungeon Masters (DMs) role.
    /// </summary>
    public class DMPermissionStrategy : IPermissionStrategy
    {
        private readonly int _userId;
        public bool CanCreateCampaigns => true;

        public bool CanCreateCharacters => true;

        /// <summary>
        /// Initializes a new instance of the DMPermissionStrategy class with the provided username
        /// </summary>
        /// <param name="username"></param>
        public DMPermissionStrategy(int userId)
        {
            _userId = userId;
        }

        public IQueryable<Character> CanView(IQueryable<Character> characters)
        {
            return characters;
        }

        public IQueryable<Character> CanEdit(IQueryable<Character> characters)
        {
            return characters;
        }

        public IQueryable<Character> CanDelete(IQueryable<Character> characters)
        {
            return characters.Where(c => c.Owner.Id == _userId);
        }

        public IQueryable<Campaign> CanView(IQueryable<Campaign> campaigns)
        {
            return campaigns.Where(c => c.AuthorId == _userId);
        }

        public IQueryable<Campaign> CanEdit(IQueryable<Campaign> campaigns)
        {
            return campaigns.Where(c => c.AuthorId == _userId);
        }

        public IQueryable<Campaign> CanDelete(IQueryable<Campaign> campaigns)
        {
            return campaigns.Where(c => c.AuthorId == _userId);
        }

        public IQueryable<Character> CanViewDetail(IQueryable<Character> characters)
        {
            return characters;
        }

        public IQueryable<Campaign> CanViewDetail(IQueryable<Campaign> campaigns)
        {
            return campaigns.Where(c => c.AuthorId == _userId);
        }
    }
}
