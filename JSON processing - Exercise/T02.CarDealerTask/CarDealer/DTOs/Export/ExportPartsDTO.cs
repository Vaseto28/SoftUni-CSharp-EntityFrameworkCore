using Newtonsoft.Json;

namespace CarDealer.DTOs.Export;

public class ExportPartsDTO
{
	[JsonProperty("Name")]
	public string Name { get; set; } = null!;

	[JsonProperty("Price")]
	public string Price { get; set; } = null!;
}

