using Microsoft.EntityFrameworkCore;
using universityAPI.models.DataModels;

namespace universityAPI.DataAccess
{
    public class UniversityDbContext: DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { 
        
        } 
        //TODO: add dbsets (tables for our data base) //SEED THE DATABASE

        public DbSet<User> ? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }
        public DbSet<Category>? Categories { get; set; }
      
        public DbSet<Student>? Students { get; set; } 
    }
}
