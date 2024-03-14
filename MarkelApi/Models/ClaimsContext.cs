using Microsoft.EntityFrameworkCore;

namespace MarkelAPI.Models
{
    public class ClaimsContext : DbContext
    {
        public ClaimsContext(DbContextOptions<ClaimsContext> options) 
            : base(options)
        {
        }

        public ClaimsContext() { }

        public DbSet<Claims> Claims { get; set; } = null;
        public DbSet<ClaimType> ClaimType { get; set; } = null;
        public DbSet<Company> Companies { get; set; } = null;
    }
}
