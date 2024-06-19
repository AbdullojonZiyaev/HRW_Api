using HRM_Project.Models;
using HRM_Project.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Models.DB
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<RefreshToken> RefreshTokens { get; set; }  
    }
}
