using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Represents a DnD campaign
    /// </summary>
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set { field = value.Validated(); } }
        public ICollection<Note> Notes { get; set; } = [];
        public int AuthorId { get; set; }
        public User Author { get; set; } = null!;
        public ICollection<CharacterStateInCampaign> CharacterStatesInCampaigns { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the Campaign class with the specified name.
        /// </summary>
        /// <param name="name">The name of the campaign. Cannot be null or empty.</param>
        /// <param name="authorId"></param>
        public Campaign(string name, int authorId)
        {
            Name = name;
            AuthorId = authorId;
        }
    }
}
