using Newtonsoft.Json;

namespace Artillery.DataProcessor.ExportDto;

public class ExportShellsDTO
{
	[JsonProperty("ShellWeight")]
	public double ShellWeight { get; set; }

	[JsonProperty("Caliber")]
	public string Caliber { get; set; } = null!;

	[JsonProperty("Guns")]
	public ExportShellsGunsDTO[]? Guns { get; set; }
}

