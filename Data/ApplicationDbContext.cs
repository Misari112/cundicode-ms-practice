using Microsoft.EntityFrameworkCore;
using ms_practice.Entities;

namespace ms_practice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ProgrammingExercise> Exercises { get; set; }
        public DbSet<CompleteProgrammingExercise> CompleteExercises { get; set; }
    }
}
