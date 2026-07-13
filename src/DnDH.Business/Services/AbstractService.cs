using DnDH.Business.UserHandling;
using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.Services
{
    /// <summary>
    /// Service base class that provides common dependencies and functionality for all services in the application.
    /// Ties the services to entity framework database context.
    /// </summary>
    public abstract class AbstractService
    {
        protected readonly IUserContext _userContext;
        protected readonly AppDbContext _dbContext;

        public AbstractService(IUserContext userContext, AppDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Checks if the user is logged in and throws an exception if not.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException"></exception>
        protected void UserLoginCheck()
        {
            if (!_userContext.IsLoggedIn)
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }
        }
    }
}
