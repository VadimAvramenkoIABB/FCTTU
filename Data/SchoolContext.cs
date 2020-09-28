using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<TeamAssignment> TeamAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Contract>().ToTable("Contract");
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Coach>().ToTable("Coach");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<TeamAssignment>().ToTable("Assignment");

            modelBuilder.Entity<TeamAssignment>()
                .HasKey(c => new { c.TeamID, c.CoachID });
        }
    }
}