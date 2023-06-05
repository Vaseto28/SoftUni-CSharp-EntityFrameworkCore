using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Common;

namespace Artillery.DataProcessor.ImportDto;

[XmlType("Shell")]
public class ImportShellsDTO
{
	[XmlElement("ShellWeight")]
	[Required]
	[Range(ValidationConstraints.ShellWeightMinValue, ValidationConstraints.ShellWeightMaxValue)]
	public double ShellWeight { get; set; }

	[XmlElement("Caliber")]
	[Required]
	[MinLength(ValidationConstraints.ShellCaliberMinLength)]
	[MaxLength(ValidationConstraints.ShellCaliberMaxLength)]
	public string Caliber { get; set; } = null!;
}

