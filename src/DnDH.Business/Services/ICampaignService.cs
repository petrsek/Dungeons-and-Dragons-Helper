using System;
using System.Collections.Generic;
using System.Text;
using DnDH.Business.DTOs;

namespace DnDH.Business.Services
{
    /// <summary>
    ///  Defines the contract for campaign-related operations within the application.
    /// </summary>
    public interface ICampaignService
    {
        /// <summary>
        /// Lists all campaigns that are available to the current user
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<CampaignDTO> GetAllAvailable();

        /// <summary>
        /// Finds campaign by its id. Returns CampaignDTO.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Used for fetching campaign details.
        /// </remarks>
        CampaignDTO GetDetail(int id);

        /// <summary>
        /// Adds new campaign with the specified name. Returns CampaignDTO of the newly created campaign.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        /// Automatically commits to database.
        /// </remarks>
        CampaignDTO Add(string name);

        /// <summary>
        /// Updates the campaign with the specified id. Returns the updated CampaignDTO.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        /// Automatically commits to database.
        /// </remarks>
        CampaignDTO Update(int id, string? name = null);

        /// <summary>
        /// Deletes the campaign with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Automatically commits to database.
        /// </remarks>
        void Delete(int id);
    }
}
