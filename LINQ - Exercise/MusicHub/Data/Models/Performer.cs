
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Data.Models;

public class Performer
{
	public Performer()
	{
		this.PerformerSongs = new HashSet<SongPerformer>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(ValidationConstraints.PerformerFirstNameMaxLength)]
	public string FirstName { get; set; }

	[Required]
	[MaxLength(ValidationConstraints.PerformerLastNameMaxLength)]
	public string LastName { get; set; }

	[Required]
	public int Age { get; set; }

	[Required]
	public decimal NetWorth { get; set; }

	public virtual ICollection<SongPerformer> PerformerSongs { get; set; }
}

