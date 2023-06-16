using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TeisterMask.Common;

namespace TeisterMask.DataProcessor.ImportDto;

[XmlType("Project")]
public class ImportProjectsDTO
{
	[XmlElement("Name")]
	[Required]
	[MinLength(ValidationConstraints.ProjectNameMinLength)]
	[MaxLength(ValidationConstraints.ProjectNameMaxLength)]
	public string Name { get; set; } = null!;

	[XmlElement("OpenDate")]
	[Required]
	public string OpenDate { get; set; } = null!;

	[XmlElement("DueDate")]
	public string? DueDate { get; set; }

	[XmlArray("Tasks")]
	public ImportTasksDTO[]? Tasks { get; set; }
}

