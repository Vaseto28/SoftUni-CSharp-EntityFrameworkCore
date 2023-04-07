using System.ComponentModel.DataAnnotations;
using P01_StudentSystem.Data.Common;

namespace P01_StudentSystem.Data.Models;

public class Course
{
	public Course()
	{
		this.Resources = new HashSet<Resource>();
		this.Homeworks = new HashSet<Homework>();
	}

	[Key]
	public int CourseId { get; set; }

	[MaxLength(ModelsValidations.CourseNameMaxLength)]
	[Required]
	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	[Required]
	public DateTime StartDate { get; set; }

	[Required]
	public DateTime EndDate { get; set; }

	[Required]
	public decimal Price { get; set; }

	public ICollection<Resource> Resources { get; set; }

	public ICollection<Homework> Homeworks { get; set; }
}