using Newtonsoft.Json;

namespace Artillery.DataProcessor.ExportDto;

public class ExportShellsGunsDTO
{
	[JsonProperty("GunType")]
	public string GunType { get; set; } = null!;

	[JsonProperty("GunWeight")]
	public int GunWeight { get; set; }

	[JsonProperty("BarrelLength")]
	public double BarrelLength { get; set; }

	[JsonProperty("Range")]
	public string Range { get; set; } = null!;
}

