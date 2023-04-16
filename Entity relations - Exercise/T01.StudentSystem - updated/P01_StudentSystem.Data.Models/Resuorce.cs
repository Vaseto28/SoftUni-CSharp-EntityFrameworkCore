using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P01_StudentSystem.Data.Common;
using P01_StudentSystem.Data.Models.Enumerators;

namespace P01_StudentSystem.Data.Models;

public class Resource
{
    [Key]
    public int ResourceId { get; set; }

    [Required]
    [MaxLength(ModelsValidations.ResourceNameMaxLength)]
    public string Name { get; set; }

    public string Url { get; set; }

    public ResourceTypes ResourceType { get; set; }

    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }

    public virtual Course Course { get; set; }
}

