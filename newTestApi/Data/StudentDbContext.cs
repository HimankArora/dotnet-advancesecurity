using Microsoft.EntityFrameworkCore;
using newTestApi.Models.Domain;

namespace newTestApi.Data
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Marks> Marks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
