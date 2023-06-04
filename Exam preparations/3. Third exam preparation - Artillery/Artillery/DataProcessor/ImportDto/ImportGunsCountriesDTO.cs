using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Artillery.DataProcessor.ImportDto;

public class ImportGunsCountriesDTO
{
	[JsonProperty("Id")]
	[Required]
	public int Id { get; set; }
}

