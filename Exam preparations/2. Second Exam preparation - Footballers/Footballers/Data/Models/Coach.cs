using System.ComponentModel.DataAnnotations;
using Footballers.Utilities;

namespace Footballers.Data.Models;

public class Coach
{
	public Coach()
	{
		this.Footballers = new HashSet<Footballer>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(ValidationConstraints.CoachNameMaxLength)]
	public string Name { get; set; } = null!;

	[Required]
	public string Nationality { get; set; } = null!;

	public virtual ICollection<Footballer> Footballers { get; set; } 
}

