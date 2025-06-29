using Microsoft.EntityFrameworkCore;

namespace Cicekci.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MONSTER\\SQLEXPRESS01;Database=Cicekci;" +
                "User ID=sa;Password=1;Trusted_Connection=False;TrustServerCertificate=True");
        }
        public DbSet<User> User { get; set; }
        
    }
}
