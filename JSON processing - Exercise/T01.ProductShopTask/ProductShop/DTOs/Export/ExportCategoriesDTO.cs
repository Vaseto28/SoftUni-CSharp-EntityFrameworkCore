using Newtonsoft.Json;

namespace ProductShop.DTOs.Export;

public class ExportCategoriesDTO
{
	[JsonProperty("category")]
	public string Category { get; set; } = null!;

	[JsonProperty("productsCount")]
	public int ProductsCount { get; set; }

	[JsonProperty("averagePrice")]
	public decimal AveragePrice { get; set; }

	[JsonProperty("totalRevenue")]
	public decimal TotalRevenue { get; set; }
}

