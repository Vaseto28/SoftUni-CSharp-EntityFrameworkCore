using Newtonsoft.Json;

namespace Theatre.DataProcessor.ExportDto;

public class ExportTheatresDTO
{
	[JsonProperty("Name")]
	public string Name { get; set; } = null!;

	[JsonProperty("Halls")]
	public sbyte Halls { get; set; }

	[JsonProperty("TotalIncome")]
	public decimal TotalIncome { get; set; }

	[JsonProperty("Tickets")]
	public ExportTicketsDTO[] Tickets { get; set; } = null!;
}

