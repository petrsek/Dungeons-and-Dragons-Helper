using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Specifies the state of a character in a campaign
    /// </summary>
    public enum CharacterState
    {
        Alive,
        Dead,
    }

    /// <summary>
    /// Represents the state of a character in a campaign
    /// </summary>
    [PrimaryKey(nameof(CharacterId), nameof(CampaignId))]
    public class CharacterStateInCampaign
    {
        public int CharacterId { get; set; }
        public int CampaignId { get; set; }
        public Character Character { get; set; } = null!;
        public Campaign Campaign { get; set; } = null!;
        public CharacterState State { get; set; } = CharacterState.Alive;

        /// <summary>
        /// Initializes a new instance of the CharacterStateInCampaign class with specified character and campaign IDs. The state is initialized to Alive by default
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="campaignId"></param>
        public CharacterStateInCampaign(int characterId, int campaignId)
        {
            CharacterId = characterId;
            CampaignId = campaignId;
        }
    }
}
