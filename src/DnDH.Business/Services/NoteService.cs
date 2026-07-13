using DnDH.Business.DTOs;
using DnDH.Business.UserHandling;
using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DnDH.Business.Services
{
    /// <summary>
    /// A concrete implementation of INoteService using Entity Framework Core.
    /// </summary>
    public class NoteService : AbstractService, INoteService
    {

        public  NoteService(IUserContext userContext, AppDbContext dbContext) : base(userContext, dbContext) { }

        public NoteDTO Add(int campaignId, string title)
        {
            UserLoginCheck();
            
            var campaign = _userContext.User!.Permissions.CanEdit(_dbContext.Campaigns.Where(c => c.Id == campaignId)).FirstOrDefault();
            if (campaign == null)
            {
                throw new InvalidOperationException($"Campaign {campaignId} not found or user does not have permission to edit it.");
            }
            else
            {
                var note = new Note(title, campaignId);
                _dbContext.Notes.Add(note);
                _dbContext.SaveChangesAsync();
                return note.MapToDTO();
            }
        }

        public void Delete(int id)
        {
            UserLoginCheck();

            var note = _dbContext.Notes.Where(n => n.Id == id).FirstOrDefault();
            if (note is null)
            {
                throw new InvalidOperationException($"Note with id {id} not found.");
            }
            else if (_userContext.User!.Permissions.CanEdit(_dbContext.Campaigns.Where(c => c.Id == note.CampaignId)).FirstOrDefault() is null)
            {
                throw new InvalidOperationException($"User does not have permission to delete note with id {id}.");
            }
            else
            {
                _dbContext.Notes.Remove(note);
                _dbContext.SaveChangesAsync();
            }
        }

        public IReadOnlyList<NoteDTO> GetCampaignNotes(int campaignId)
        {
            UserLoginCheck();
            var campaign = _userContext.User!.Permissions.CanViewDetail(_dbContext.Campaigns.Where(c => c.Id == campaignId)).FirstOrDefault();
            if (campaign is null)
            {
                throw new InvalidOperationException($"Campaign with id {campaignId} not found or user does not have permission to view its detail.");
            }
            else
            {
                var notes = _dbContext.Notes.Where(n => n.CampaignId == campaignId);
                return notes.Select(n => n.MapToDTO()).ToList();
            }
        }

        public NoteDTO GetDetail(int id)
        {
            UserLoginCheck();

            var note = _dbContext.Notes.Where(n => n.Id == id).FirstOrDefault();
            if (note is null)
            {
                throw new InvalidOperationException($"Note with id {id} not found.");
            }
            else if (_userContext.User!.Permissions.CanViewDetail(_dbContext.Campaigns.Where(c => c.Id == note.CampaignId)).FirstOrDefault() is null)
            {
                throw new InvalidOperationException($"User does not have permission to view detail of note with id {id}.");
            }
            else
            {
                return note.MapToDTO();
            }
        }

        public NoteDTO Update(int id, string? title = null, string? text = null)
        {
            var note = _dbContext.Notes.Where(n => n.Id == id).FirstOrDefault();
            if (note is null)
            {
                throw new InvalidOperationException($"Note with id {id} not found.");
            }
            else if (_userContext.User!.Permissions.CanViewDetail(_dbContext.Campaigns.Where(c => c.Id == note.CampaignId)).FirstOrDefault() is null)
            {
                throw new InvalidOperationException($"User does not have permission to view detail of note with id {id}.");
            }

            if (title is not null)
            {
                note.Title = title; // Input is validated in setter
            }
            if (text is not null) 
            {
                note.Text = text; // Input is validated in setter
            }
            note.UpdateTimeOfUpdate();
            _dbContext.SaveChangesAsync();
            return note.MapToDTO();
        }
    }
}
