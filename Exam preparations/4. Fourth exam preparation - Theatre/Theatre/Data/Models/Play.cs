using System.ComponentModel.DataAnnotations;
using Theatre.Common;
using Theatre.Data.Models.Enums;

namespace Theatre.Data.Models;

public class Play
{
	public Play()
	{
		this.Casts = new HashSet<Cast>();
		this.Tickets = new HashSet<Ticket>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(ValidationConstraints.PlayTitleMaxLength)]
	public string Title { get; set; } = null!;

	[Required]
	public TimeSpan Duration { get; set; }

	[Required]
	public float Rating { get; set; }

	[Required]
	public Genre Genre { get; set; }

	[Required]
	[MaxLength(ValidationConstraints.PlayDescriptionMaxLength)]
	public string Description { get; set; } = null!;

	[Required]
	[MaxLength(ValidationConstraints.PlayScreenwriterMaxLength)]
	public string Screenwriter { get; set; } = null!;

	public virtual ICollection<Cast> Casts { get; set; }

	public virtual ICollection<Ticket> Tickets { get; set; }
}

