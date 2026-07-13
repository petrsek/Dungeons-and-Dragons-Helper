using DnDH.Business.DTOs;
using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.UserHandling.PermisionStrategy
{
    /// <summary>
    /// Defines a strategy for determining user permissions
    /// </summary>
    public interface IPermissionStrategy
    {
        /// <summary>
        /// Indicates whether the user can create campaigns.
        /// </summary>
        public bool CanCreateCampaigns { get; }

        /// <summary>
        /// Indicates whether the user can create characters.
        /// </summary>
        public bool CanCreateCharacters { get; }

        /// <summary>
        /// Filters the given characters based on the view permissions of the user.
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public IQueryable<Character> CanView(IQueryable<Character> characters);

        /// <summary>
        /// Filters the given characters based on the detail view permissions of the user.
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public IQueryable<Character> CanViewDetail(IQueryable<Character> characters);

        /// <summary>
        /// Filters the given characters based on the edit permissions of the user.
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public IQueryable<Character> CanEdit(IQueryable<Character> characters);

        /// <summary>
        /// Filters the given characters based on the delete permissions of the user.
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public IQueryable<Character> CanDelete(IQueryable<Character> characters);

        /// <summary>
        /// Filters the given campaigns based on the view permissions of the user.
        /// </summary>
        /// <param name="campaigns"></param>
        /// <returns></returns>
        public IQueryable<Campaign> CanView(IQueryable<Campaign> campaigns);

        /// <summary>
        /// Filters the given campaigns based on the detail view permissions of the user.
        /// </summary>
        /// <param name="campaigns"></param>
        /// <returns></returns>
        public IQueryable<Campaign> CanViewDetail(IQueryable<Campaign> campaigns);

        /// <summary>
        /// Filters the given campaigns based on the edit permissions of the user.
        /// </summary>
        /// <param name="campaigns"></param>
        /// <returns></returns>
        public IQueryable<Campaign> CanEdit(IQueryable<Campaign> campaigns);

        /// <summary>
        /// Filters the given campaigns based on the delete permissions of the user.
        /// </summary>
        /// <param name="campaigns"></param>
        /// <returns></returns>
        public IQueryable<Campaign> CanDelete(IQueryable<Campaign> campaigns);

    }
}
