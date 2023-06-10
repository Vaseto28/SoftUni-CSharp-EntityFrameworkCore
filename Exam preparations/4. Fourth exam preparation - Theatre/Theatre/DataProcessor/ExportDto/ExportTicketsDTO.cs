using Newtonsoft.Json;

namespace Theatre.DataProcessor.ExportDto;

public class ExportTicketsDTO
{
	[JsonProperty("Price")]
	public decimal Price { get; set; }

	[JsonProperty("RowNumber")]
	public sbyte RowNumber { get; set; }
}

