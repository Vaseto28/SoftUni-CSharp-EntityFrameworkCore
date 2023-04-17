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

    public DbSet<Student> Students { get; set; } = null!;

    public DbSet<Course> Courses { get; set; } = null!;

    public DbSet<Resource> Resources { get; set; } = null!;

    public DbSet<Homework> Homeworks { get; set; } = null!;

    public DbSet<StudentCourse> StudentCourses { get; set; } = null!;

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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .HasMany(x => x.Homeworks)
            .WithOne(x => x.Student)
            .HasForeignKey(x => x.HomeworkId);

        modelBuilder.Entity<Student>()
            .Property(x => x.Name)
            .HasColumnType("nvarchar");

        modelBuilder.Entity<Course>()
            .HasMany(x => x.Homeworks)
            .WithOne(x => x.Course)
            .HasForeignKey(x => x.HomeworkId);

        modelBuilder.Entity<Course>()
            .HasMany(x => x.Resources)
            .WithOne(x => x.Course)
            .HasForeignKey(x => x.ResourceId);

        modelBuilder.Entity<Course>(x =>
        {
            x.Property(x => x.Name)
                .HasColumnType("nvarchar");

            x.Property(x => x.Description)
                .HasColumnType("nvarchar");
        });

        modelBuilder.Entity<Resource>(x =>
        {
            x.Property(x => x.Name)
                .HasColumnType("nvarchar");

            x.Property(x => x.Url)
                .HasColumnType("varchar");
        });

        modelBuilder.Entity<Homework>()
            .Property(x => x.Content)
            .HasColumnType("varchar");

        modelBuilder.Entity<StudentCourse>()
            .HasKey(x => new { x.StudentId, x.CourseId });
    }
} 