using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Theatre.Common;

namespace Theatre.DataProcessor.ImportDto;

[XmlType("Cast")]
public class ImportCastsDTO
{
    [XmlElement("FullName")]
    [Required]
    [MinLength(ValidationConstraints.CastFullNameMinLength)]
    [MaxLength(ValidationConstraints.CastFullNameMaxLength)]
    public string FullName { get; set; } = null!;

    [XmlElement("IsMainCharacter")]
    [Required]
    public bool IsMainCharacter { get; set; }

    [XmlElement("PhoneNumber")]
    [Required]
    [RegularExpression(ValidationConstraints.CastPhoneNumberPattern)]
    public string PhoneNumber { get; set; } = null!;

    [XmlElement("PlayId")]
    [Required]
    public int PlayId { get; set; }
}

