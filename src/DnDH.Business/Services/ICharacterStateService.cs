using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using DnDH.Business.DTOs;

namespace DnDH.Business.Services
{
    public interface ICharacterStateService
    {
            /// <summary>
            /// Updates the character's state in a specific campaign.
            /// </summary>
            /// <param name="characterId"></param>
            /// <param name="campaignId"></param>
            void UpdateCharacterState(int characterId, int campaignId, CharacterState state = CharacterState.Alive);

            IReadOnlyList<CharacterStateDTO> FindCharactersInCampaign(int campaignId);
            IReadOnlyList<CharacterListItemDTO> FindCharactersNotInCampaignByState(int campaignId);

            /// <summary>
            /// Removes a character from a campaign.
            /// </summary>
            void RemoveCharacterFromCampaign(int characterId, int campaignId);


    }
}
