namespace Footballers.Utilities;

public static class ValidationConstraints
{
	//Footballer
	public const int FootballerNameMinLength = 2;
	public const int FootballerNameMaxLength = 40;

	//Team
	public const int TeamNameMinLength = 3;
	public const int TeamNameMaxLength = 40;
	public const int TeamNationalityMinLength = 2;
	public const int TeamNationalityMaxLength = 40;
	public const string TeamNameRegex = "^[A-Za-z\\d\\s\\.\\-]{3,}$";

    //Coach
    public const int CoachNameMinLength = 2;
	public const int CoachNameMaxLength = 40;
}

