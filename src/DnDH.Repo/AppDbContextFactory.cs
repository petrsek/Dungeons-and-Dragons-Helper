using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DnDH.Repo
{
    /// <summary>
    /// Factory class for creating instances of AppDbContext during design-time operations
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=dndh.db")
                .Options;
            return new AppDbContext(options);
        }
    }
}
