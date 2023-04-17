using System.ComponentModel.DataAnnotations;
using P01_StudentSystem.Data.Common;

namespace P01_StudentSystem.Data.Models;

public class Student
{
    [Key]
    public int StudentId { get; set; }

    [MaxLength(ModelsValidations.StudentNameMaxLength)]
    [Required]
    public string Name { get; set; } = null!;

    [MaxLength(ModelsValidations.StudentPhoneNumberLength)]
    public string? PhoneNumber { get; set; }

    public DateTime RegisteredOn { get; set; }

    public DateTime Birthday  { get; set; }

    public ICollection<Homework> Homeworks { get; set; }
}