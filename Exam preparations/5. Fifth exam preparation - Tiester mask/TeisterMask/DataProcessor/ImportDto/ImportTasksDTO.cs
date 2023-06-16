using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TeisterMask.Common;

namespace TeisterMask.DataProcessor.ImportDto;

[XmlType("Task")]
public class ImportTasksDTO
{
	[XmlElement("Name")]
	[Required]
	[MinLength(ValidationConstraints.TaskNameMinLength)]
	[MaxLength(ValidationConstraints.TaskNameMaxLength)]
	public string Name { get; set; } = null!;

	[XmlElement("OpenDate")]
	[Required]
	public string OpenDate { get; set; } = null!;

	[XmlElement("DueDate")]
	[Required]
	public string DueDate { get; set; } = null!;

	[XmlElement("ExecutionType")]
	[Required]
	[Range(0 ,3)]
	public int ExecutionType { get; set; }

	[XmlElement("LabelType")]
	[Required]
	[Range(0, 4)]
	public int LabelType { get; set; }
}

