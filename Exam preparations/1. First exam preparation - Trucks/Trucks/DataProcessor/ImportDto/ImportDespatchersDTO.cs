using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Trucks.Utilities;

namespace Trucks.DataProcessor.ImportDto;

[XmlType("Despatcher")]
public class ImportDespatchersDTO
{
	[XmlElement("Name")]
	[Required]
	[MinLength(ValidationConstraints.DespatcherNameMinLength)]
	[MaxLength(ValidationConstraints.DespatcherNameMaxLength)]
	public string Name { get; set; } = null!;

	[XmlElement("Position")]
	public string? Position { get; set; }

	[XmlArray("Trucks")]
	public ImportTrucksDTO[]? Trucks { get; set; }
}

