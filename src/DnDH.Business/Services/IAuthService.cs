using DnDH.Business.DTOs;
using DnDH.Business.UserHandling;
using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.Services
{
    /// <summary>
    /// Defines methods to handle user authentication and authorization.
    /// </summary>
    public interface IAuthService
    {
 
        /// <summary>
        /// Attempts to authenticate a user with the provided username and password. 
        /// If successful, returns a UserDTO representing the authenticated user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public UserDTO FindUserByLogin(string username, string password);
    }
}
