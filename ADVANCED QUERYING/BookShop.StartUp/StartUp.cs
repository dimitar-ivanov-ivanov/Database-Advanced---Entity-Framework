namespace BookShop
{
    using BookShop.Data;
    using BookShop.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);
                //Console.WriteLine(GetBooksByAgeRestriction(db, Console.ReadLine()));
                //Console.WriteLine(GetGoldenBooks(db));
                //Console.WriteLine(GetBooksByPrice(db));
                //Console.WriteLine(GetBooksNotRealeasedIn(db,int.Parse(Console.ReadLine())));
                //Console.WriteLine(GetBooksByCategory(db,Console.ReadLine()));
                //Console.WriteLine(GetBooksReleasedBefore(db, Console.ReadLine()));
                //Console.WriteLine(GetAuthorNamesEndingIn(db,Console.ReadLine()));
                //Console.WriteLine(GetBookTitlesContaining(db,Console.ReadLine()));
                //Console.WriteLine(GetBooksByAuthor(db,Console.ReadLine()));
                //Console.WriteLine(CountBooks(db, int.Parse(Console.ReadLine())));
                //Console.WriteLine(CountCopiesByAuthor(db));
                //Console.WriteLine(GetTotalProfitByCategory(db));
                //Console.WriteLine(GetMostRecentBooks(db));
                //IncreasePrices(db);
                var removedBooks = RemoveBooks(db);
                Console.WriteLine($"{removedBooks} books were deleted");
            }
        }

        private static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToArray();

            context.Books.RemoveRange(books);
            context.SaveChanges();
            return books.Length;
        }

        private static void IncreasePrices(BookShopContext context)
        {
            const int increase = 5;

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToArray();

            for (int i = 0; i < books.Length; i++)
            {
                books[i].Price += increase;
            }
            context.SaveChanges();
        }

        private static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    Name = c.Name,
                    //Count = c.CategoryBooks.Count,
                    Books = c.CategoryBooks
                    .Select(cb => cb.Book)
                    .OrderByDescending(b => b.ReleaseDate).Take(3)
                });               

            var builder = new StringBuilder();

            foreach (var c in categories)
            {
                builder.AppendLine($"--{c.Name}");
                foreach (var b in c.Books)
                {
                    builder.AppendLine($"{b.Title} ({b.ReleaseDate.Value.Year})");
                }
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    TotalProfit = c.CategoryBooks
                    .Sum(b => b.Book.Copies * b.Book.Price)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.Name);

            var builder = new StringBuilder();

            foreach (var c in categories)
            {
                builder.AppendLine($"{c.Name} ${c.TotalProfit:f2}");
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string CountCopiesByAuthor(BookShopContext context)
        {
            var author = context.Authors
                .Select(a => new
                {
                    FullName = $"{a.FirstName} {a.LastName}",
                    Count = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.Count);

            var builder = new StringBuilder();
            foreach (var a in author)
            {
                builder.AppendLine($"{a.FullName} - {a.Count}");
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count().ToString();

            return booksCount;
        }

        private static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var bookAuthors = context.Books
                .Where(b => b.Author.LastName.StartsWith(input, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    TitleAuthor = $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})"
                });

            var builder = new StringBuilder();
            foreach (var b in bookAuthors)
            {
                builder.AppendLine(b.TitleAuthor);
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var titles = context.Books
                .Where(b => b.Title.IndexOf(input, StringComparison.CurrentCultureIgnoreCase) != -1)
                .Select(b => b.Title)
                .OrderBy(t => t);

            var builder = new StringBuilder();

            foreach (var t in titles)
            {
                builder.AppendLine(t);
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input, StringComparison.CurrentCultureIgnoreCase))
                .Select(a => new
                {
                    FullName = $"{a.FirstName} {a.LastName}"
                })
                .OrderBy(a => a.FullName);

            var builder = new StringBuilder();
            foreach (var a in authors)
            {
                builder.AppendLine(a.FullName);
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var relaseDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var builder = new StringBuilder();
            var books = context.Books
                .Where(b => b.ReleaseDate < relaseDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    Output = $"{b.Title} - {b.EditionType} - ${b.Price:f2}"
                });

            foreach (var b in books)
            {
                builder.AppendLine(b.Output);
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string GetBooksByCategory(BookShopContext context, string input)
        {
            var args = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var books = context.Books
                .Select(b => new
                {
                    Categories = b.BookCategories
                    .Select(bc => bc.Category.Name),
                    Title = b.Title
                });

            var titles = new List<string>();
            var builder = new StringBuilder();

            foreach (var b in books)
            {
                foreach (var category in b.Categories)
                {
                    if (args.FindIndex(x => x.Equals(category,
                        StringComparison.OrdinalIgnoreCase)) != -1)
                    {
                        titles.Add(b.Title);
                        break;
                    }
                }
            }

            titles = titles.OrderBy(t => t).ToList();

            foreach (var title in titles)
            {
                builder.AppendLine(title);
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title);

            var builder = new StringBuilder();
            foreach (var b in books)
            {
                builder.AppendLine(b);
            }

            builder = builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string GetBooksByAgeRestriction
            (BookShopContext context, string command)
        {

            var commandArgs = command.ToLower().ToCharArray();
            commandArgs[0] = commandArgs[0].ToString().ToUpper()[0];

            command = new string(commandArgs);
            var ageRestriction = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command);

            var titles = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(t => t);

            var builder = new StringBuilder();
            foreach (var title in titles)
            {
                builder.AppendLine(title);
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string GetGoldenBooks(BookShopContext context)
        {
            var titles = context.Books
                .Where(b => b.EditionType == EditionType.Gold
                && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title);

            var builder = new StringBuilder();
            foreach (var title in titles)
            {
                builder.AppendLine(title);
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        private static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    TitlePrice = $"{b.Title} - ${b.Price:f2}"
                });

            var builder = new StringBuilder();

            foreach (var book in books)
            {
                builder.AppendLine(book.TitlePrice);
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
    }
}
