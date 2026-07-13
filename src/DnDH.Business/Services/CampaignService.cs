using DnDH.Business.DTOs;
using DnDH.Business.UserHandling;
using DnDH.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.Services
{
    /// <summary>
    /// A concrete implementation of ICampaignService using Entity Framework Core.
    /// </summary>
    public class CampaignService : AbstractService, ICampaignService
    {
        public CampaignService(IUserContext userContext, AppDbContext dbContext) : base(userContext, dbContext) { }

        public CampaignDTO Add(string name)
        {
            UserLoginCheck();

            if (!_userContext.User!.Permissions.CanCreateCampaigns)
            {
                throw new InvalidOperationException("User does not have permission to create campaigns.");
            }

            var campaign = new Campaign(name, _userContext.User.Id);
            _dbContext.Campaigns.Add(campaign);
            _dbContext.SaveChangesAsync();
            return campaign.MapToDTO();
        }

        public void Delete(int id)
        {
            UserLoginCheck();

            var campaign = _userContext.User!.Permissions.CanDelete(_dbContext.Campaigns.Where(c => c.Id == id)).FirstOrDefault();
            if (campaign is null)
            {
                throw new InvalidOperationException($"Campaign with id {id} not found or user does not have permission to delete it.");
            }
            else
            {
                _dbContext.Campaigns.Remove(campaign);
                _dbContext.SaveChangesAsync();
            }
        }

        public IReadOnlyList<CampaignDTO> GetAllAvailable()
        {
            UserLoginCheck();
            var campaigns = _userContext.User!.Permissions.CanView(_dbContext.Campaigns);
            return campaigns.Select(c => c.MapToDTO()).ToList();
        }

        public CampaignDTO GetDetail(int id)
        {
            UserLoginCheck();
            var campaign = _userContext.User!.Permissions.CanViewDetail(_dbContext.Campaigns.Where(c => c.Id == id)).FirstOrDefault();
            if (campaign is null)
            {
                throw new InvalidOperationException($"Campaign with id {id} not found or user does not have permission to view it's detail.");
            }
            else
            {
                return campaign.MapToDTO();
            }
        }

        public CampaignDTO Update(int id, string? name = null)
        {
            UserLoginCheck();
            var campaign = _userContext.User!.Permissions.CanViewDetail(_dbContext.Campaigns.Where(c => c.Id == id)).FirstOrDefault();
            if (campaign is null)
            {
                throw new InvalidOperationException($"Campaign with id {id} not found or user does not have permission to view it's detail.");
            }
            else if (name is not null)
            {
                campaign.Name = name!; // sting validation is handled by the setter of the Name property
                _dbContext.SaveChangesAsync();
            }
            return campaign.MapToDTO();
        }
    }
}
