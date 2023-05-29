using Newtonsoft.Json;

namespace Footballers.DataProcessor.ExportDto;

public class ExportTeamsDTO
{
	[JsonProperty("Name")]
	public string Name { get; set; } = null!;

	[JsonProperty("Footballers")]
	public ExportFootballersDTO[] Footballers { get; set; } = null!;
}

