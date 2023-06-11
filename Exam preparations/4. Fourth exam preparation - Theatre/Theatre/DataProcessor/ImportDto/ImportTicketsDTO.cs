using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Theatre.Common;

namespace Theatre.DataProcessor.ImportDto;

public class ImportTicketsDTO
{
	[JsonProperty("Price")]
	[Required]
	[Range((double)ValidationConstraints.TicketPriceMinValue, (double)ValidationConstraints.TicketPriceMaxValue)]
	public decimal Price { get; set; }

	[JsonProperty("RowNumber")]
	[Required]
	[Range(ValidationConstraints.TicketRowNumberMinValue, ValidationConstraints.TicketRowNumberMaxValue)]
	public sbyte RowNumber { get; set; }

	[JsonProperty("PlayId")]
	[Required]
	public int PlayId { get; set; }
}

