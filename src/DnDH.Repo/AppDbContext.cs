using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Custom DbContext for the DnD Helper application
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<CharacterStateInCampaign> CharacterStatesInCampaigns { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Spellcasting> CharacterSpellcasting { get; set; }
        public DbSet<Spell> Spells {  get; set; }

        /// <summary>
        /// Override OnModelCreating to configure the model relationships and owned entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure Username is unique
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
            });

            // Configure foreign key from Campaign to User (Author)
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasOne(c => c.Author)
                    .WithMany(u => u.Campaigns)
                    .HasForeignKey(c => c.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Character>(entity =>
            {
                // Configure owned entities for Character => 1 table in database, but multiple classes in code for easier management
                entity.OwnsOne(c => c.Abilities);
                entity.OwnsOne(c => c.Inventory);
                entity.OwnsOne(c => c.Health);

                // Configure foreign key from Character to User (Owner)
                entity.HasOne(c => c.Owner)
                    .WithMany(u => u.Characters)
                    .HasForeignKey(c => c.OwnerId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            // Configure foreign key from Note to Campaign
            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasOne(n => n.Campaign)
                    .WithMany(c => c.Notes)
                    .HasForeignKey(n => n.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure many-to-many relationship between Character and Campaign through CharacterStateInCampaign
            modelBuilder.Entity<CharacterStateInCampaign>(entity =>
            {
                entity.HasKey(e => new { e.CharacterId, e.CampaignId });

                entity.HasOne(e => e.Campaign)
                    .WithMany(c => c.CharacterStatesInCampaigns)
                    .HasForeignKey(e => e.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Character)
                    .WithMany(c => c.CharacterStatesInCampaigns)
                    .HasForeignKey(e => e.CharacterId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
