using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class Team
{
    public Team()
    {
        this.Players = new HashSet<Player>();
        this.HomeGames = new HashSet<Game>();
        this.AwayGames = new HashSet<Game>();
    }

    [Key]
    public int TeamId { get; set; }

    [Required]
    [MaxLength(ValidationParams.TeamNameMaxLength)]
    public string Name { get; set; }

    [MaxLength(ValidationParams.TeamLogoUrlMaxLength)]
    public string LogoUrl { get; set; }

    [MaxLength(ValidationParams.TeamInitialsMaxLength)]
    public string Initials { get; set; }

    public decimal Budget { get; set; }


    public int PrimaryKitColorId { get; set; }

    public virtual Color PrimaryKitColor { get; set; }

    public int SecondaryKitColorId { get; set; }

    public virtual Color SecondaryKitColor { get; set; }

    [ForeignKey(nameof(Town))]
    public int TownId { get; set; }

    public virtual Town Town { get; set; }

    public virtual ICollection<Player> Players { get; set; }

    public virtual ICollection<Game> HomeGames { get; set; }

    public virtual ICollection<Game> AwayGames { get; set; }
}