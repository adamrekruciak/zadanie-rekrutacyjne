using Microsoft.EntityFrameworkCore;

namespace zadanie_rekrutacyjne.Model
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentItem> DocumentItems { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
    }
}
