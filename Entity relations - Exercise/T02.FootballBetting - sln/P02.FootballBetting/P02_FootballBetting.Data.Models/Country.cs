using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class Country
{
	public Country()
	{
		this.Towns = new HashSet<Town>();
	}

	[Key]
	public int CountryId { get; set; }

	[Required]
	[MaxLength(ValidationParams.CountryNameMaxLength)]
	public string Name { get; set; }

	public virtual ICollection<Town> Towns { get; set; }
}

