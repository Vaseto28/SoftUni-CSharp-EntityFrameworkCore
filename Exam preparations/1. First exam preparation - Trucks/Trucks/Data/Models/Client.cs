using System.ComponentModel.DataAnnotations;
using Trucks.Utilities;

namespace Trucks.Data.Models;

public class Client
{
	public Client()
	{
		this.ClientsTrucks = new HashSet<ClientTruck>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(ValidationConstraints.ClientNameMaxLength)]
	public string Name { get; set; } = null!;

	[Required]
	[MaxLength(ValidationConstraints.ClientNationalityMaxLength)]
	public string Nationality { get; set; } = null!;

	[Required]
	public string Type { get; set; } = null!;

	public virtual ICollection<ClientTruck> ClientsTrucks  { get; set; }
}

