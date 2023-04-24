namespace BookShop
{
    using System.Text;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            //int input = int.Parse(Console.ReadLine());
            Console.WriteLine(RemoveBooks(db));
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            bool hasParsed = Enum.TryParse(typeof(AgeRestriction), command, true, out object AgeRestrictionObj);
            AgeRestriction ageRestriction;
            if (hasParsed)
            {
                ageRestriction = (AgeRestriction)AgeRestrictionObj;

                var bookTitles = context.Books
                .Where(x => x.AgeRestriction == ageRestriction)
                .OrderBy(x => x.Title)
                .Select(x => x.Title)
                .ToArray();

                return string.Join(Environment.NewLine, bookTitles);
            }

            return null;
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            string[] bookTitles = context.Books
                .Where(x => x.EditionType == (EditionType)2 && x.Copies < 5000)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title)
                .ToArray();

            return String.Join(Environment.NewLine, bookTitles);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Price > 40)
                .OrderByDescending(x => x.Price)
                .Select(x => new
                {
                    x.Title,
                    x.Price
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return sb.ToString().Trim();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            DateTime dateTimeStart = new DateTime(year, 1, 1);
            DateTime dateTimeEnd = new DateTime(year, 12, 31);

            var books = context.Books
                .Where(x => x.ReleaseDate < dateTimeStart || x.ReleaseDate > dateTimeEnd)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower())
                .ToArray();

            var books = context.Books
                .Where(x => x.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(x => x.Title)
                .Select(x => x.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            int year = int.Parse(date.Split('-', StringSplitOptions.RemoveEmptyEntries)[2]);
            int month = int.Parse(date.Split('-', StringSplitOptions.RemoveEmptyEntries)[1]);
            int day = int.Parse(date.Split('-', StringSplitOptions.RemoveEmptyEntries)[0]);
            DateTime givenDate = new DateTime(year, month, day);

            var books = context.Books
                .Where(x => x.ReleaseDate < givenDate)
                .OrderByDescending(x => x.ReleaseDate)
                .Select(x => new
                {
                    x.Title,
                    x.EditionType,
                    x.Price
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString().Trim();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var names = context.Authors
                .Where(x => x.FirstName.EndsWith(input))
                .OrderBy(x => x.FirstName + " " + x.LastName)
                .Select(x => new
                {
                    FullName = $"{x.FirstName} {x.LastName}"
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var name in names)
            {
                sb.AppendLine(name.FullName);
            }

            return sb.ToString().Trim();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            string[] titles = context.Books
                .Where(x => x.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(x => x.Title)
                .Select(x => x.Title)
                .ToArray();

            return string.Join(Environment.NewLine, titles);
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(x => x.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(x => x.BookId)
                .Select(x => new
                {
                    x.Title,
                    AuthorName = $"{x.Author.FirstName} {x.Author.LastName}"
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorName})");
            }

            return sb.ToString().Trim();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books
                .Where(x => x.Title.Length > lengthCheck)
                .Count();
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(x => new
                {
                    AuthorName = $"{x.FirstName} {x.LastName}",
                    BookCoppies = x.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(x => x.BookCoppies)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var author in authors)
            {
                sb.AppendLine($"{author.AuthorName} - {author.BookCoppies}");
            }

            return sb.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categoriesProfits = context.Categories
                .Select(x => new
                {
                    x.Name,
                    Profit = x.CategoryBooks.Sum(x => x.Book.Price * x.Book.Copies)
                })
                .OrderByDescending(x => x.Profit)
                .ThenBy(x => x.Name)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var categoryProfit in categoriesProfits)
            {
                sb.AppendLine($"{categoryProfit.Name} ${categoryProfit.Profit:f2}");
            }

            return sb.ToString().Trim();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var mostRecentBooks = context.Categories
                .Select(x => new
                {
                    x.Name,
                    Books = x.CategoryBooks.OrderByDescending(x => x.Book.ReleaseDate).Select(x => new { x.Book.Title, ReleaseDate = x.Book.ReleaseDate.Value.Year }).Take(3)
                })
                .OrderBy(x => x.Name)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var c in mostRecentBooks)
            {
                sb.AppendLine($"--{c.Name}");
                foreach (var book in c.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate})");
                }
            }

            return sb.ToString().Trim();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.ReleaseDate.Value.Year < 2010)
                .ToArray();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Copies < 4200)
                .ToList();

            int booksToDeleteCount = books.Count;

            foreach (var book in books)
            {
                context.Books.Remove(book);
            }

            context.SaveChanges();
            return booksToDeleteCount;
        }
    }
}


