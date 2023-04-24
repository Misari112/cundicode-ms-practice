using Microsoft.EntityFrameworkCore;
using ms_practice.Models;

namespace ms_practice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<Code> Codes { get; set; }
        public DbSet<Stdin> Stdins { get; set; }
    }
}
