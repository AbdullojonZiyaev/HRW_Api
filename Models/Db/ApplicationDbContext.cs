using HRM_Project.Models.Common;
using HRM_Project.Models.Types;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Models.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options ) : base (options)
        {
            //Database.EnsureCreated();
            Database.Migrate ();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<ActType> ActTypes { get; set; }
        public DbSet<Act> Acts { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<ReferenceType> ReferenceTypes { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<TimeSheetType> TimeSheetTypes { get; set; }

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
            base.OnModelCreating (modelBuilder);

            modelBuilder.Entity<Department> ()
                .HasMany (d => d.Divisions)
                .WithOne (dv => dv.Department)
                .HasForeignKey (dv => dv.DepartmentId)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Division> ()
                .HasMany (dv => dv.Employees)
                .WithOne (e => e.Division)
                .HasForeignKey (e => e.DivisionId)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee> ()
                .HasOne (e => e.Company)
                .WithMany (c => c.Employees)
                .HasForeignKey (e => e.CompanyId)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee> ()
                .HasOne (e => e.Department)
                .WithMany (d => d.Employees)
                .HasForeignKey (e => e.DepartmentId)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee> ()
                .HasOne (e => e.Position)
                .WithMany (p => p.Employees)
                .HasForeignKey (e => e.PositionId)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Vacancy> ()
                .HasOne (v => v.Company)
                .WithMany (c => c.Vacancies)
                .HasForeignKey (v => v.CompanyId)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Vacancy> ()
                .HasOne (v => v.Department)
                .WithMany (d => d.Vacancies)
                .HasForeignKey (v => v.DepartmentId)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Vacancy> ()
                .HasOne (v => v.Division)
                .WithMany (dv => dv.Vacancies)
                .HasForeignKey (v => v.DivisionId)
                .OnDelete (DeleteBehavior.Restrict);

            // Указание типа данных для Salary
            modelBuilder.Entity<Employee> ()
                .Property (e => e.Salary)
                .HasColumnType ("decimal(18,2)");

            // Указание типа данных для других моделей
            modelBuilder.Entity<Vacancy> ()
                .Property (v => v.Salary)
                .HasColumnType ("decimal(18,2)");
        }
    }
}
