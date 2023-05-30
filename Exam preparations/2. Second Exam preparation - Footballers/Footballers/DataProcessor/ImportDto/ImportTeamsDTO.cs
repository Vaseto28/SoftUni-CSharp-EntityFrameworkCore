using System.ComponentModel.DataAnnotations;
using Footballers.Utilities;
using Newtonsoft.Json;

namespace Footballers.DataProcessor.ImportDto;

public class ImportTeamsDTO
{
	[JsonProperty("Name")]
	[Required]
	[MinLength(ValidationConstraints.TeamNameMinLength)]
	[MaxLength(ValidationConstraints.TeamNameMaxLength)]
    [RegularExpression(ValidationConstraints.TeamNameRegex)]
    public string Name { get; set; } = null!;

	[JsonProperty("Nationality")]
	[Required]
	[MinLength(ValidationConstraints.TeamNationalityMinLength)]
	[MaxLength(ValidationConstraints.TeamNationalityMaxLength)]
	public string Nationality { get; set; } = null!;

	[JsonProperty("Trophies")]
	[Required]
	public int Trophies { get; set; }

	[JsonProperty("Footballers")]
	[Required]
	public int[] Footballers { get; set; } = null!;
}

