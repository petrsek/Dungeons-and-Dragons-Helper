using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Specifies the possible roles a user can have within the application.
    /// </summary>
    public enum UserRole
    {
        Player,
        DungeonMaster
    }

    /// <summary>
    /// Represent a user of the application
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Player;
        public ICollection<Campaign> Campaigns { get; set; } = [];
        public ICollection<Character> Characters { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the User class with a specified username and role (UserRole.Player by default).
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        public User(string username, UserRole role = UserRole.Player)
        {
            Username = username.Validated();
            Role = role;
        }
    }
}
