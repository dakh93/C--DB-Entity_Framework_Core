using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models.Enums;
using BookShop.StartUp;
using System.Text;
using System.Linq;

//using (var db = new BookShopContext())
//{
//    DbInitializer.ResetDatabase(db);
//}



/*-------------------------------------------------------------------*/
//5.Book Titles by Category
//using (var db = new BookShopContext())
//{
//    Console.WriteLine("Type down the categories with space between them: ");
//    var input = Console.ReadLine();

//    var books = GetBooksByCategory(db, input);

//    Console.WriteLine(books);
//}

//string GetBooksByCategory(BookShopContext db, string? input)
//{

//    if (string.IsNullOrEmpty(input))
//    {
//        throw new Exception(ErrorMessages.EmptyInput);
//    }

//    var sb = new StringBuilder();

//    var categories = input.Split(" ").ToArray();

//    var books = db.BooksCategories
//       .Select(b => new
//       {
//           Book = b.Book,
//           Category = b.Category,
//       })
//       .Where(b => b.Category.Name.ToLower() == categories[0].ToLower())
//       .ToList();
       

//    for (int i = 1; i < categories.Length; i++)
//    {
//        var current = db.BooksCategories
//            .Select(b => new
//            {
//                Book = b.Book,
//                Category = b.Category,
//            })
//            .Where(b => b.Category.Name.ToLower() == categories[i].ToLower());

//        foreach (var book in current)
//        {
//            books.Add(book);
//        }

            
//    }

//    foreach (var book in books.OrderBy(b => b.Book.Title))
//    {
//        sb.AppendLine(book.Book.Title);
//    }

//    return sb.ToString().Trim();

//}
/*-------------------------------------------------------------------*/
//4.Not Released In
//using (var db = new BookShopContext())
//{
//    Console.Write("Enter year: ");
//    int bookYear = int.Parse(Console.ReadLine());

//    var books = GetBooksNotReleasedIn(db, bookYear);

//    Console.WriteLine(books);
//}

//string GetBooksNotReleasedIn(BookShopContext db, int bookYear)
//{
//    var sb = new StringBuilder();

//    var books = db.Books
//        .Where(b => b.ReleaseDate != null &&
//                    b.ReleaseDate.Value.Year != bookYear)
//        .OrderBy(b => b.BookId)
//        .ToList();

//    foreach (var book in books) sb.AppendLine(book.Title);

//    return sb.ToString().Trim();
//}
/*-------------------------------------------------------------------*/
//3.Books by Price
//using (var db = new BookShopContext())
//{
//    var booksByPrice = GetBooksByPrice(db);
//    Console.WriteLine(booksByPrice);
//}

//string GetBooksByPrice(BookShopContext db)
//{
//    var sb = new StringBuilder();
//    var bookPrice = 40;

//    var books = db.Books
//        .Where(b => b.Price > bookPrice)
//        .OrderByDescending(b => b.Price)
//        .Select(b => new
//        {
//            Title = b.Title,
//            Price = b.Price,
//        })
//        .ToList();

//    foreach (var b in books)
//    {
//        sb.AppendLine($"{b.Title} - ${b.Price}");
//    }

//    return sb.ToString().Trim();
//}
/*-------------------------------------------------------------------*/
////2.Golden Books
//using (var db = new BookShopContext())
//{

//    var goldenBooks = GetGoldenBooks(db);

//    Console.WriteLine(goldenBooks);
//}

//string GetGoldenBooks(BookShopContext db)
//{
//    var sb = new StringBuilder();
//    var bookCopies = 5000;

//    var books = db.Books
//        .Where(b => b.Copies < bookCopies)
//        .OrderBy(b => b.BookId)
//        .ToList();

//    foreach (var book in books) sb.AppendLine(book.Title);

//    return sb.ToString().Trim();
//}
/*-------------------------------------------------------------------*/
////1.Age Restriction
//using (var db = new BookShopContext())
//{
//    var command = Console.ReadLine().ToLower();
//    var bookTitlesByAgeRestriction = GetBooksByAgeRestriction(db, command);

//    Console.WriteLine(bookTitlesByAgeRestriction);
//}

//string GetBooksByAgeRestriction(BookShopContext db, string command)
//{
//    var sb = new StringBuilder();
//    var formattedCmd = command[0].ToString().ToUpper() + command.Substring(1);

//    var cmdToEnum = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), formattedCmd);

//    var books = db.Books
//        .Where(b => b.AgeRestriction == cmdToEnum)
//        .Select(b => b.Title)
//        .OrderBy(b => b)
//        .ToList();

//    if (books == null)
//    {
//        throw new Exception(ErrorMessages.NoBooksFound);
//    }

//    foreach (var book in books) sb.AppendLine(book);

//    return sb.ToString().Trim();

//}
/*-------------------------------------------------------------------*/