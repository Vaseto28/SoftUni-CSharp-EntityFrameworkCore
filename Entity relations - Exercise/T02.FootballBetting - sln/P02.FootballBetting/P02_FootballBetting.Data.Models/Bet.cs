using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Data.Models.Enumerators;

namespace P02_FootballBetting.Data.Models;

public class Bet
{
	[Key]
	public int BetId { get; set; }

	public decimal Amount { get; set; }

	public Predictions Prediction { get; set; }

	public DateTime DateTime { get; set; }

	[ForeignKey(nameof(User))]
	public int UserId { get; set; }

	public virtual User User { get; set; }

	[ForeignKey(nameof(Game))]
	public int GameId { get; set; }

	public virtual Game Game { get; set; }
}

