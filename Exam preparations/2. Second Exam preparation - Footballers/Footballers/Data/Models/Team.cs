using System.ComponentModel.DataAnnotations;
using Footballers.Utilities;

namespace Footballers.Data.Models;

public class Team
{
	public Team()
	{
		this.TeamsFootballers = new HashSet<TeamFootballer>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(ValidationConstraints.TeamNameMaxLength)]
	public string Name { get; set; } = null!;

	[Required]
	[MaxLength(ValidationConstraints.TeamNationalityMaxLength)]
	public string Nationality { get; set; } = null!;

	[Required]
	public int Trophies { get; set; }

	public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; }
}

