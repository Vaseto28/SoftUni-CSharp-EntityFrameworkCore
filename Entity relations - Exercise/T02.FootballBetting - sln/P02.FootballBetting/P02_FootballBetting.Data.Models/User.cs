using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class User
{
	public User()
	{
		this.Bets = new HashSet<Bet>();
	}

	[Key]
	public int UserId { get; set; }

	[Required]
	[MaxLength(ValidationParams.UserUsernameMaxLength)]
	public string Username { get; set; }

	[Required]
	[MaxLength(ValidationParams.UserPasswordMaxLength)]
	public string Password { get; set; }

	[Required]
	public string Email { get; set; }

	[Required]
	[MaxLength(ValidationParams.UserNameMaxLength)]
	public string Name { get; set; }

	public decimal Balance { get; set; }

	public virtual ICollection<Bet> Bets { get; set; }
}

