using System.ComponentModel.DataAnnotations;
using P01_StudentSystem.Data.Common;

namespace P01_StudentSystem.Data.Models;

public class Student
{
    public Student()
    {
        this.Homeworks = new HashSet<Homework>();
        this.StudentCourses = new HashSet<StudentCourse>();
    }

    [Key]
    public int StudentId { get; set; }

    [Required]
    [MaxLength(ModelsValidations.StudentNameMaxLength)]
    public string Name { get; set; }

    [MaxLength(ModelsValidations.StudentPhoneNumberLength)]
    public string PhoneNumber { get; set; }

    public DateTime RegisteredOn { get; set; }

    public DateTime Birthday { get; set; }

    public virtual ICollection<Homework> Homeworks { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; }
}