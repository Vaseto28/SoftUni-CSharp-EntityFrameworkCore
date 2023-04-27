
using Newtonsoft.Json;

namespace ProductShop.DTOs.Import;

public class ImportCategoriesProductsDTO
{
	[JsonProperty("CategoryId")]
	public int CategoryId { get; set; }

	[JsonProperty("ProductId")]
	public int ProductId { get; set; }
}

