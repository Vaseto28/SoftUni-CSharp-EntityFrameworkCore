using System.ComponentModel.DataAnnotations;
using P01_StudentSystem.Data.Common;

namespace P01_StudentSystem.Data.Models;

public class Course
{
    public Course()
    {
        this.Resources = new HashSet<Resource>();
        this.Homeworks = new HashSet<Homework>();
        this.StudentCourses = new HashSet<StudentCourse>();
    }

    [Key]
    public int CourseId { get; set; }

    [Required]
    [MaxLength(ModelsValidations.CourseNameMaxLength)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public decimal Price { get; set; }

    public virtual ICollection<Resource> Resources { get; set; }

    public virtual ICollection<Homework> Homeworks { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; }
}