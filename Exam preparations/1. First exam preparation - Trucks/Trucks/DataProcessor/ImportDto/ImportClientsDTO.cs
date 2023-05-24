using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Trucks.Utilities;

namespace Trucks.DataProcessor.ImportDto;

public class ImportClientsDTO
{
	[JsonProperty("Name")]
	[Required]
	[MinLength(ValidationConstraints.ClientNameMinLength)]
	[MaxLength(ValidationConstraints.ClientNameMaxLength)]
	public string Name { get; set; } = null!;

	[JsonProperty("Nationality")]
	[Required]
	[MinLength(ValidationConstraints.ClientNationalityMinLength)]
	[MaxLength(ValidationConstraints.ClientNationalityMaxLength)]
	public string Nationality { get; set; } = null!;

	[JsonProperty("Type")]
	[Required]
	public string Type { get; set; } = null!;

	[JsonProperty("Trucks")]
	public int[] Trucks { get; set; } = null!;
}

