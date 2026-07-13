using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used to transfer data related to a campaign between different layers of the application
    /// </summary>
    public sealed record CampaignDTO(int Id, string Name, int AuthorId);
}
