using Newtonsoft.Json;
using Trucks.Data.Models.Enums;

namespace Trucks.DataProcessor.ExportDto;

public class ExportTrucksDTO
{
	[JsonProperty("TruckRegistrationNumber")]
	public string TruckRegistrationNumber { get; set; } = null!;

	[JsonProperty("VinNumber")]
	public string VinNumber { get; set; } = null!;

	[JsonProperty("TankCapacity")]
	public int TankCapacity { get; set; }

	[JsonProperty("CargoCapacity")]
	public int CargoCapacity { get; set; }

	[JsonProperty("CategoryType")]
	public string CategoryType { get; set; } = null!;

	[JsonProperty("MakeType")]
	public string MakeType { get; set; } = null!;
}

