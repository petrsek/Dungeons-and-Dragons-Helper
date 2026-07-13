using System;
using System.Collections.Generic;
using System.Text;
using DnDH.Repo;

namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used to transfer character state information
    /// </summary>
    /// <param name="characterId"></param>
    /// <param name="characterName"></param>
    /// <param name="campaignId"></param>
    /// <param name="State"></param>
    public record CharacterStateDTO(int characterId, string characterName, int campaignId, CharacterState State)
    {
    }
}
