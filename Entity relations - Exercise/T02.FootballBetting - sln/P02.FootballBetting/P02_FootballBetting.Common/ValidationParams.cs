namespace P02_FootballBetting.Common;

public static class ValidationParams
{
	// Team
	public const int TeamNameMaxLength = 30;
	public const int TeamLogoUrlMaxLength = 2048;
	public const int TeamInitialsMaxLength = 5;

	// Color
	public const int ColorNameMaxLength = 15;

	// Town
	public const int TownNameMaxLength = 50;

	// Country
	public const int CountryNameMaxLength = 60;

	// Player
	public const int PlayerNameMaxLength = 100;

	// Position
	public const int PositionNameMaxLength = 50;

	// Game
	public const int GameResultMaxLength = 7;

	// User
	public const int UserUsernameMaxLength = 30;
	public const int UserPasswordMaxLength = 20;
	public const int UserNameMaxLength = 100;
}