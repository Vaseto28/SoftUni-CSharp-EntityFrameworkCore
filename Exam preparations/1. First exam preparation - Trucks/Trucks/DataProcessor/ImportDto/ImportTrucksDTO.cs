using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Trucks.Utilities;

namespace Trucks.DataProcessor.ImportDto;

[XmlType("Truck")]
public class ImportTrucksDTO
{
	[XmlElement("RegistrationNumber")]
	[Required]
	[RegularExpression("[A-Z]{2}\\d{4}[A-Z]{2}")]
	[MinLength(ValidationConstraints.TruckRegistrationNumberRequiredLength)]
	[MaxLength(ValidationConstraints.TruckRegistrationNumberRequiredLength)]
	public string RegistrationNumber { get; set; } = null!;

	[XmlElement("VinNumber")]
	[Required]
    [MinLength(ValidationConstraints.TruckVinNumberRequiredLength)]
    [MaxLength(ValidationConstraints.TruckVinNumberRequiredLength)]
    public string VinNumber { get; set; } = null!;

	[XmlElement("TankCapacity")]
	[Range(ValidationConstraints.TruckTankCapacityMinValue, ValidationConstraints.TruckTankCapacityMaxValue)]
	public int TankCapacity { get; set; }

	[XmlElement("CargoCapacity")]
    [Range(ValidationConstraints.TruckCargoCapacityMinValue, ValidationConstraints.TruckCargoCapacityMaxValue)]
    public int CargoCapacity { get; set; }

	[XmlElement("CategoryType")]
	[Required]
	public string CategoryType { get; set; } = null!;

	[XmlElement("MakeType")]
	[Required]
	public string MakeType { get; set; } = null!;
}

