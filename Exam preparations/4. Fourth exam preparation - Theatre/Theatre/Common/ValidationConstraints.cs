namespace Theatre.Common;

public static class ValidationConstraints
{
	//Theare
	public const int TheatreNameMinLength = 4;
	public const int TheatreNameMaxLength = 30;
	public const int TheatreNumberOfHallsMinValue = 1;
	public const int TheatreNumberOfHallsMaxValue = 10;
	public const int TheatreDirectorMinLength = 4;
	public const int TheatreDirectorMaxLength = 30;

	//Play
	public const int PlayTitleMinLength = 4;
	public const int PlayTitleMaxLength = 50;
	public const float PlayRatingMinValue = 0.00f;
	public const float PlayRatingMaxValue = 10.00f;
	public const int PlayDescriptionMaxLength = 700;
	public const int PlayScreenwriterMinLength = 4;
	public const int PlayScreenwriterMaxLength = 30;

	//Cast
	public const int CastFullNameMinLength = 4;
	public const int CastFullNameMaxLength = 30;
	public const string CastPhoneNumberPattern = "^\\+44\\-[\\d]{2}\\-[\\d]{3}\\-[\\d]{4}$";

	//Ticket
	public const decimal TicketPriceMinValue = 1.00m;
	public const decimal TicketPriceMaxValue = 100.00m;
	public const sbyte TicketRowNumberMinValue = 1;
	public const sbyte TicketRowNumberMaxValue = 10;
}

