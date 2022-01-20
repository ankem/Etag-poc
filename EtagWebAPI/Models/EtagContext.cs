using Microsoft.EntityFrameworkCore;
namespace EtagWebAPI.Models
{
    public class EtagContext : DbContext
    {
        public EtagContext(DbContextOptions<EtagContext> options)
            : base(options)
        {
        }

        public DbSet<EtagCache> EtagItems { get; set; } = null!;
    }
}
