using DnDH.Business.UserHandling.PermisionStrategy;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used to transfer information about the user, including their permissions.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Username"></param>
    /// <param name="Permissions"></param>
    public sealed record UserDTO(int Id, string Username, IPermissionStrategy Permissions);
}
