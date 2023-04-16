using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Common;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data;

public class StudentSystemContext : DbContext
{
    public StudentSystemContext()
    {

    }

    public StudentSystemContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Resource> Resources { get; set; }

    public DbSet<Homework> Homeworks { get; set; }

    public DbSet<StudentCourse> StudentCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasMany(x => x.Homeworks)
            .WithOne(x => x.Student)
            .HasForeignKey(x => x.HomeworkId);

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasMany(x => x.Homeworks)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.HomeworkId);

            entity.HasMany(x => x.Resources)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.ResourceId);
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(x => new { x.StudentId, x.CourseId });
        });

        base.OnModelCreating(modelBuilder);
    }
}