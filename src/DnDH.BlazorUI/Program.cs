using DnDH.BlazorUI.Components;
using DnDH.Business.Services;
using DnDH.Business.UserHandling;
using DnDH.Repo;
using Microsoft.EntityFrameworkCore;

namespace DnDH.BlazorUI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddScoped<IUserContext, UserContext>()
            .AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=dndh.db"))
            .AddScoped<IAuthService, UsernameAuthService>()
            .AddScoped<ICampaignService, CampaignService>()
            .AddScoped<ICharacterService, CharacterService>()
            .AddScoped<ISpellService, SpellService>()
            .AddScoped<INoteService, NoteService>()
            .AddScoped<ICharacterStateService, CharacterStateService>();

        var app = builder.Build();

        // Ensure database is created and migrations are applied
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.MigrateAsync();
            #if DEBUG
            SeedDatabase(db); // Seed initial data for debugging purposes
            #endif
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
    
    #if DEBUG
    /// <summary>
    /// Adds initial data to the database for testing and debugging purposes, if there aren't already any entries.
    /// </summary>
    /// <param name="db"></param>
    static void SeedDatabase(AppDbContext db)
    {
        if (!db.Users.Any())
        {
            db.Users.Add(new User("player1", UserRole.Player) {Id = 1});
            db.Users.Add(new User("dm1", UserRole.DungeonMaster) {Id = 2});
            db.Users.Add(new User("player2", UserRole.Player) {Id = 3});
            db.Users.Add(new User("dm2", UserRole.DungeonMaster) {Id = 4});
        }
        if (!db.Spells.Any())
        {
            db.Spells.Add(new Spell { Id = 1, Name = "Fireball", Level = 3, Range = "150 feet", Components = "V, S, M (a tiny ball of bat guano and sulfur)", Description = "A bright streak flashes from your pointing finger to a point you choose within range and then blossoms with a low roar into an explosion of flame. Each creature in a 20-foot-radius sphere centered on that point must make a Dexterity saving throw. A target takes 8d6 fire damage on a failed save, or half as much damage on a successful one." });
            db.Spells.Add(new Spell { Id = 2, Name = "Mage Hand", Level = 0, Range = "30 feet", Components = "V, S", Description = "A spectral, floating hand appears at a point you choose within range. The hand lasts for the duration or until you dismiss it as an action. The hand vanishes if it is ever more than 30 feet away from you or if you cast this spell again." });
            db.Spells.Add(new Spell { Id = 3, Name = "Light", Level = 0, Range = "60 feet", Components = "V, M (a firefly or phosphorescent moss)", Description = "You touch a torch, lantern, or other light source to ignite it, if it is not already lit. You can also use your action to snuff out the light." });
        }
        if (!db.Campaigns.Any())
        {
            db.Campaigns.Add(new Campaign("kampan1", 2) {Id = 1});
            db.Campaigns.Add(new Campaign("kampan2", 4) {Id = 2});
        }
        if (!db.Characters.Any())
        {
            db.Characters.Add(new Character("Grog Strongjaw", 1) { Id = 1, Class = "Barbarian", Level = 5 });
            db.Characters.Add(new Character("Vex'ahlia", 3) { Id = 2, Class = "Ranger", Level = 5 });
        }
        db.SaveChangesAsync();
    }
    #endif
}
