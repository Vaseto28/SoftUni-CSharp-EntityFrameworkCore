using Newtonsoft.Json;

namespace CarDealer.DTOs.Export;

public class ExportCarsDTO
{
	[JsonProperty("Make")]
	public string Make { get; set; } = null!;

	[JsonProperty("Model")]
	public string Model { get; set; } = null!;

	[JsonProperty("TraveledDistance")]
	public long TraveledDistance { get; set; }
}

