using Newtonsoft.Json;

namespace CarDealer.DTOs.Export;

public class ExportCarsPartsDTO
{
	public ExportCarsPartsDTO()
	{
		this.Parts = new List<ExportPartsDTO>();
	}

	[JsonProperty("car")]
	public ExportCarsDTO Car { get; set; } = null!;

	[JsonProperty("parts")]
	public ICollection<ExportPartsDTO> Parts { get; set; }
}

