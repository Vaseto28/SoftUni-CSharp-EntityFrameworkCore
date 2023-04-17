using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P01_StudentSystem.Data.Common;
using P01_StudentSystem.Data.Models.Enumerators;

namespace P01_StudentSystem.Data.Models;

public class Resource
{
	[Key]
	public int ResourceId { get; set; }

	[MaxLength(ModelsValidations.ResourceNameMaxLength)]
	[Required]
	public string Name { get; set; } = null!;

	public string Url { get; set; }

	public ResourceTypeEnum ResourceType { get; set; }

	[ForeignKey(nameof(Course))]
	public int CourseId { get; set; }

	public Course Course { get; set; }
}

