namespace BookShop.Models.Validatons;

public static class ValidationConstraints
{
	// Author
	public const int AuthorFirstNameMaxLength = 50;
	public const int AuthorLastNameMaxLength = 50;

	// Book
	public const int BookTitleMaxLength = 50;
	public const int BookDescriptionMaxLength = 1000;

	// Category
	public const int CategoryNameMaxLength = 50;
}

