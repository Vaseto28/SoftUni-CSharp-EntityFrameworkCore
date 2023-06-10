using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Theatre.Common;

namespace Theatre.DataProcessor.ImportDto;

[XmlType("Play")]
public class ImportPlaysDTO
{
	[XmlElement("Title")]
	[Required]
	[MinLength(ValidationConstraints.PlayTitleMinLength)]
	[MaxLength(ValidationConstraints.PlayTitleMaxLength)]
	public string Title { get; set; } = null!;

	[XmlElement("Duration")]
	[Required]
	public string Duration { get; set; } = null!;

	[XmlElement("Raiting")]
	[Required]
	[Range(ValidationConstraints.PlayRatingMinValue, ValidationConstraints.PlayRatingMaxValue)]
	public float Rating { get; set; }

	[XmlElement("Genre")]
	[Required]
	public string Genre { get; set; } = null!;

	[XmlElement("Description")]
	[Required]
	[MaxLength(ValidationConstraints.PlayDescriptionMaxLength)]
	public string Description { get; set; } = null!;

	[XmlElement("Screenwriter")]
	[Required]
	[MinLength(ValidationConstraints.PlayScreenwriterMinLength)]
	[MaxLength(ValidationConstraints.PlayScreenwriterMaxLength)]
    public string Screenwriter { get; set; } = null!;
}

