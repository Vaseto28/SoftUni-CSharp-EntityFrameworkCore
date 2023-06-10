using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Theatre.Common;

namespace Theatre.DataProcessor.ImportDto;

public class ImportTheatresDTO
{
	[JsonProperty("Name")]
	[Required]
	[MinLength(ValidationConstraints.TheatreNameMinLength)]
	[MaxLength(ValidationConstraints.TheatreNameMaxLength)]
    public string Name { get; set; } = null!;

	[JsonProperty("NumberOfHalls")]
	[Required]
	[Range(ValidationConstraints.TheatreNumberOfHallsMinValue, ValidationConstraints.TheatreNumberOfHallsMaxValue)]
	public sbyte NumberOfHalls { get; set; }

	[JsonProperty("Director")]
	[Required]
	[MinLength(ValidationConstraints.TheatreDirectorMinLength)]
	[MaxLength(ValidationConstraints.TheatreDirectorMaxLength)]
	public string Director { get; set; } = null!;

	[JsonProperty("Tickets")]
	public ImportTicketsDTO[]? Tickets { get; set; }
}

