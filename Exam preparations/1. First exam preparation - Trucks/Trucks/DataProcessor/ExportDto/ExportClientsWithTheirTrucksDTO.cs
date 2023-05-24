using Newtonsoft.Json;

namespace Trucks.DataProcessor.ExportDto;

public class ExportClientsWithTheirTrucksDTO
{
	[JsonProperty("Name")]
	public string Name { get; set; } = null!;

	[JsonProperty("Trucks")]
	public ExportTrucksDTO[] Trucks { get; set; } = null!;
}

