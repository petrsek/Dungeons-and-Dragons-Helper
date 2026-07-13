using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used to transfer a subset of character data for listing purposes
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="OwnerId"></param>
    /// <param name="Name"></param>
    /// <param name="Level"></param>
    /// <param name="Class"></param>
    /// <param name="Race"></param>
    public sealed record CharacterListItemDTO(int Id, int OwnerId, string Name, int Level, string Class, string Race);
}
