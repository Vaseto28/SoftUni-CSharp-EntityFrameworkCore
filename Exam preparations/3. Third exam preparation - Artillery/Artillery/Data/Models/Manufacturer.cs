using System.ComponentModel.DataAnnotations;
using Artillery.Common;
using Microsoft.EntityFrameworkCore;

namespace Artillery.Data.Models;

public class Manufacturer
{
	public Manufacturer()
	{
		this.Guns = new HashSet<Gun>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(ValidationConstraints.ManufacturerNameMaxLength)]
	public string ManufacturerName { get; set; } = null!;

	[Required]
	[MaxLength(ValidationConstraints.ManufacturerFoundedMaxLength)]
	public string Founded { get; set; } = null!;

	public virtual ICollection<Gun> Guns { get; set; }
}

