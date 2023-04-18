using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class Game
{
	public Game()
	{
		this.PlayersStatistics = new HashSet<PlayerStatistic>();
		this.Bets = new HashSet<Bet>();
    }

	[Key]
	public int GameId { get; set; }

	public int HomeTeamId { get; set; }

	[Required]
	public virtual Team HomeTeam { get; set; }

	public int AwayTeamId { get; set; }

	[Required]
	public virtual Team AwayTeam { get; set; }

	public byte HomeTeamGoals { get; set; }

	public byte AwayTeamGoals { get; set; }

	public DateTime DateTime { get; set; }

	public double HomeTeamBetRate { get; set; }

	public double AwayTeamBetRate { get; set; }

	public double DrawBetRate { get; set; }

	[Required]
	[MaxLength(ValidationParams.GameResultMaxLength)]
	public string Result { get; set; }

	public virtual ICollection<PlayerStatistic> PlayersStatistics { get; set; }

	public virtual ICollection<Bet> Bets { get; set; }
}

