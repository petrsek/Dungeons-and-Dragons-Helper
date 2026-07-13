using DnDH.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.UserHandling
{
    /// <summary>
    /// Defines properties and methods for a user context, which stores information about the current user, such as their ID and permissions.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Event that is triggered when the user context changes, allowing components to react to changes in the user's authentication state.
        /// </summary>
        public event Action? OnUserChanged;
        public UserDTO? User { get; }
        public bool IsLoggedIn { get; }

        /// <summary>
        /// Logs in the user represented by the DTO
        /// </summary>
        /// <param name="user"></param>
        public void Login(UserDTO user);

        /// <summary>
        /// Logs the current user out of the application and ends the user session.
        /// </summary>
        public void Logout();
    }
}
