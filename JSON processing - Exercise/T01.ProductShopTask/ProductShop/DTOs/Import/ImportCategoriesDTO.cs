using Newtonsoft.Json;

namespace ProductShop.DTOs.Import;

public class ImportCategoriesDTO
{
	[JsonProperty("name")]
	public string Name { get; set; } = null!;
}

