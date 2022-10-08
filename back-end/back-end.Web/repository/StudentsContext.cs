using Microsoft.EntityFrameworkCore;
using students_db.Models;

namespace students_db.Repository;

public class StudentsContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    public StudentsContext(DbContextOptions<StudentsContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable(@"Server=localhost; Database=students_db; Uid=root; Pwd=123456;");

            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 11)),
                options => options.EnableRetryOnFailure()
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasKey(s => s.RA);
    }
}
