using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.UserHandling.PermisionStrategy
{
    /// <summary>
    /// Creates permission strategies based on user roles.
    /// </summary>
    /// <remarks>
    /// This factory class abstracts the creation of permission strategies, allowing for easy extension in the future if new roles are added.
    /// In current version, only single role is supported per user, but the structure allows for future support of multiple roles if needed.
    /// </remarks>
    public static class PermissionStrategyFactory
    {
        /// <summary>
        /// Creates an instance implementing IPermissionStrategy based on the provided user roles and ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IPermissionStrategy CreatePermissionStrategy(int id, params UserRole[] roles)
        {
            if (roles == null || roles.Length == 0)
            {
                throw new ArgumentException("At least one role must be provided", nameof(roles));
            }
            else if (roles.Length > 1)
            {
                throw new NotImplementedException("Multiple roles are not supported in current version.");
            }            

            var role = roles[0];
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), id, "User ID must be a positive integer");
            }
            return role switch
            {
                UserRole.DungeonMaster => new DMPermissionStrategy(id),
                UserRole.Player => new PlayerPermissionStrategy(id),
                _ => throw new ArgumentException("Invalid role", nameof(role))
            };
        }
    }
}
