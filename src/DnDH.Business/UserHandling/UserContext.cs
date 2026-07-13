using DnDH.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.UserHandling
{
    /// <summary>
    /// A concrete implementation of the IUserContext interface.
    /// </summary>
    public class UserContext : IUserContext
    {
        public event Action? OnUserChanged;
        public UserDTO? User { get; private set; }

        public bool IsLoggedIn => User is not null;

        public void Login(UserDTO user)
        {
            User = user;
            OnUserChanged?.Invoke();
        }

        public void Logout()
        {
            User = null;
            OnUserChanged?.Invoke();
        }
    }
}
