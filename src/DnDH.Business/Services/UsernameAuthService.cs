using DnDH.Business.DTOs;
using DnDH.Business.UserHandling;
using DnDH.Business.UserHandling.PermisionStrategy;
using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.Services
{
    /// <summary>
    /// Provides a simple username authentication mechanism
    /// </summary>
    /// <remarks>
    /// Used for demonstration purposes only on a local machine.
    /// </remarks>
    public class UsernameAuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;

        public UsernameAuthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Attempts to authenticate a user with the provided username, password is discarded
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public UserDTO FindUserByLogin(string username, string password)
        {
            var user = _dbContext.Users
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
                throw new Exception("User not found");

           return user.MapToDTO();
        }
        
    }
}
