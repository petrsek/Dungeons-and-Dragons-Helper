using DnDH.Business.DTOs;
using DnDH.Business.UserHandling;
using DnDH.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDH.Business.Services
{
    /// <summary>
    /// A concrete implementation of ICharacterStateService using Entity Framework
    /// </summary>
    public class CharacterStateService : AbstractService, ICharacterStateService
    {
        public CharacterStateService(IUserContext userContext, AppDbContext dbContext) : base(userContext, dbContext)
        {
        }

        public IReadOnlyList<CharacterStateDTO> FindCharactersInCampaign(int campaignId)
        {
            UserLoginCheck();
            
            var campaign = _userContext.User!.Permissions.CanViewDetail(_dbContext.Campaigns.Where(c => c.Id == campaignId)).FirstOrDefault();
            if (campaign == null)
            {
                throw new InvalidOperationException($"Campaign {campaignId} not found or user does not have permission to view it.");
            }

            var states = _dbContext.CharacterStatesInCampaigns
                .Include(cs => cs.Character)
                .Where(cs => cs.CampaignId == campaignId);

            return states.Select(cs => cs.MapToDTO()).ToList();
        }

        public IReadOnlyList<CharacterListItemDTO> FindCharactersNotInCampaignByState(int campaignId)
        {
            UserLoginCheck();

            var campaign = _userContext.User!.Permissions.CanEdit(_dbContext.Campaigns.Where(c => c.Id == campaignId)).FirstOrDefault();
            if (campaign == null)
            {
                throw new InvalidOperationException($"Campaign {campaignId} not found or user does not have permission to edit it.");
            }

            var states = _dbContext.CharacterStatesInCampaigns
                .Include(cs => cs.Character)
                .Where(cs => cs.CampaignId == campaignId);


            var charactersInCampaign = states.Select(cs => cs.CharacterId).ToHashSet();

            var availableCharacters = _userContext.User!.Permissions.CanViewDetail(_dbContext.Characters)
                .Where(c => !charactersInCampaign.Contains(c.Id));

            
            return availableCharacters.Select(c => c.MapToListItemDTO()).ToList();
        }

        public void UpdateCharacterState(int characterId, int campaignId, CharacterState state = CharacterState.Alive)
        {
            UserLoginCheck();

            var campaign = _userContext.User!.Permissions.CanEdit(_dbContext.Campaigns.Where(c => c.Id == campaignId)).FirstOrDefault();
            if (campaign == null)
            {
                throw new InvalidOperationException($"Campaign {campaignId} not found or user does not have permission to edit it.");
            }

            var character = _userContext.User!.Permissions.CanViewDetail(_dbContext.Characters.Where(c => c.Id == characterId)).FirstOrDefault();
            if (character == null)
            {
                throw new InvalidOperationException($"Character {characterId} not found or user does not have permission to view it.");
            }

            var characterState = _dbContext.CharacterStatesInCampaigns
                .FirstOrDefault(cs => cs.CharacterId == characterId && cs.CampaignId == campaignId);

            if (characterState == null)
            {
                characterState = new CharacterStateInCampaign(characterId, campaignId);
                characterState.State = state;
                _dbContext.CharacterStatesInCampaigns.Add(characterState);
            }
            else
            {
                characterState.State = state;
            }

            _dbContext.SaveChangesAsync();
        }

        public void RemoveCharacterFromCampaign(int characterId, int campaignId)
        {
            UserLoginCheck();

            var campaign = _userContext.User!.Permissions.CanEdit(_dbContext.Campaigns.Where(c => c.Id == campaignId)).FirstOrDefault();
            if (campaign == null)
            {
                throw new InvalidOperationException($"Campaign {campaignId} not found or user does not have permission to edit it.");
            }

            var characterState = _dbContext.CharacterStatesInCampaigns
                .FirstOrDefault(cs => cs.CharacterId == characterId && cs.CampaignId == campaignId);

            if (characterState != null)
            {
                _dbContext.CharacterStatesInCampaigns.Remove(characterState);
                _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Character {characterId} is not in campaign {campaignId}.");
            }
        }
    }
}
