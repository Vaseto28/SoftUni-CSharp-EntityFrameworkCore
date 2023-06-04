using System.ComponentModel.DataAnnotations;
using Artillery.Common;
using Newtonsoft.Json;

namespace Artillery.DataProcessor.ImportDto;

public class ImportGunsDTO
{
	[JsonProperty("ManufacturerId")]
	[Required]
	public int ManufacturerId { get; set; }

	[JsonProperty("GunWeight")]
	[Required]
	[Range(ValidationConstraints.GunWeightMinValue, ValidationConstraints.GunWeightMaxValue)]
	public int GunWeight { get; set; }

	[JsonProperty("BarrelLength")]
	[Required]
	[Range(ValidationConstraints.GunBarrelLengthMinValue, ValidationConstraints.GunBarrelLengthMaxValue)]
	public double BarrelLength { get; set; }

	[JsonProperty("NumberBuild")]
	public int? NumberBuild { get; set; }

	[JsonProperty("Range")]
	[Required]
	[Range(ValidationConstraints.GunRangeMinValue, ValidationConstraints.GunRangeMaxValue)]
	public int Range { get; set; }

	[JsonProperty("GunType")]
	[Required]
	public string GunType { get; set; } = null!;

	[JsonProperty("ShellId")]
	[Required]
	public int ShellId { get; set; }

	[JsonProperty("Countries")]
	public ImportGunsCountriesDTO[]? Countries { get; set; }
}

