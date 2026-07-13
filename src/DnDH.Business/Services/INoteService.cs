using DnDH.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.Services
{
    /// <summary>
    /// Defines the contract for note management operations.
    /// </summary>
    public interface INoteService
    {
        /// <summary>
        /// Lists all notes in campaign
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        IReadOnlyList<NoteDTO> GetCampaignNotes(int campaignId);

        /// <summary>
        /// Creates a new note with the specified title and associates it with the given campaign ID.
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// Automatically commits to database.
        /// </remarks>
        public NoteDTO Add(int campaignId, string title);

        /// <summary>
        /// Finds note by its id. Returns NoteDTO if found, null otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Used for fetching note details.
        /// </remarks>
        NoteDTO GetDetail(int id);

        /// <summary>
        /// Updates the notes content. Time of update is automatically set to current time. Null values are not updated.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks>
        /// Automatically commits to database.
        /// </remarks>
        public NoteDTO Update(int id, string? title = null, string? text = null);

        /// <summary>
        /// Deletes the note with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Automatically commits to database.
        /// </remarks>
        public void Delete(int id);
    }
}
