using Newtonsoft.Json;

namespace CarDealer.DTOs.Import;

public class ImportSuppliersDTO
{
	[JsonProperty("name")]
	public string Name { get; set; } = null!;

	[JsonProperty("isImporter")]
	public bool IsImporter { get; set; }
}

