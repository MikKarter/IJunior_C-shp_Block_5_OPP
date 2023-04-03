using System;
using System.Collections.Generic;


namespace _6.Task_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();

            bool isWork = true;

            const int AddBookCommand = 1;
            const int ShowAllBookCommand = 2;
            const int RemoveBookCommand = 3;
            const int ShowBookByParametrCommand = 4;
            const int ExitCommand = 5;

            while (isWork)
            {
                Console.WriteLine(AddBookCommand + " - add book");
                Console.WriteLine(ShowAllBookCommand + " - show all book in storage");
                Console.WriteLine(RemoveBookCommand + " - remove book");
                Console.WriteLine(ShowBookByParametrCommand + " - show book by parametr");
                Console.WriteLine(ExitCommand + " - EXIT");

                int.TryParse(Console.ReadLine(), out int key);

                switch (key)
                {
                    case AddBookCommand:
                        storage.AddBook();
                        break;
                    case ShowAllBookCommand:
                        storage.ShowAllBooks();
                        break;
                    case RemoveBookCommand:
                        storage.RemoveBook();
                        break;
                    case ShowBookByParametrCommand:
                        storage.ShowBooksByParametr();
                        break;
                    case ExitCommand:
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Input correct number!");
                        break;
                }
            }
        }
    }

    class Book
    {
        public Book(string title, string author, int releaseYear)
        {
            Title = title;
            Author = author;
            ReleaseYear = releaseYear;
        }

        public string Title { get; private set; }
        public string Author { get; private set; }
        public int ReleaseYear { get; private set; }
    }

    class Storage
    {
        private List<Book> _books = new List<Book>();

        public Storage()
        {
            _books = new List<Book>();
        }

        public void AddBook()
        {
            Console.WriteLine("Input title");
            string title = Console.ReadLine();
            Console.WriteLine("Input author");
            string author = Console.ReadLine();
            Console.WriteLine("Input year of release");
            int.TryParse(Console.ReadLine(), out int releaseYear);            
            _books.Add(new Book(title, author, releaseYear));
        }

        public void ShowAllBooks()
        {
            foreach (var book in _books)
            {
                Console.WriteLine($"Title: {book.Title} | Author: {book.Author} | Year: {book.ReleaseYear}");
            }
        }

        public void RemoveBook()
        {
            int i = 0;
            Console.WriteLine("Enter the nubmer book for removed");

            foreach (Book book in _books)
            {                
                Console.WriteLine($"{i + 1}. {book.Title} ({book.Author},{book.ReleaseYear})");
                i++;
            }

            int.TryParse(Console.ReadLine(), out int resault);
            _books.RemoveAt(resault-1);            
        }

        public void ShowBooksByParametr()
        {
            const int SearchBookForTitleCommand = 1;
            const int SearchBookForAuthorCommand = 2;
            const int SearchBookForYearComand = 3;

            Console.WriteLine($"Push {SearchBookForTitleCommand} to search book for title.");
            Console.WriteLine($"Push {SearchBookForAuthorCommand} to search book for author.");
            Console.WriteLine($"Push {SearchBookForYearComand} to search book for relese year.");
            int.TryParse(Console.ReadLine(), out int key);

            switch (key)
            {
                case SearchBookForTitleCommand:
                    ShowBooksByParametrTitle();
                    break;
                case SearchBookForAuthorCommand:
                    ShowBooksByParametrAuthor();
                    break;
                case SearchBookForYearComand:
                    ShowBooksByParametrYear();
                    break;
                default:
                    {
                        Console.WriteLine("Input correct number!");
                        break;
                    }
            }
        }

        public void ShowBooksByParametrTitle()
        {
            Console.WriteLine("Enter the title:");
            string title = Console.ReadLine();
            List<Book> titleBooks = _books.FindAll(Book => Book.Title.ToLower() == title.ToLower());

            if (titleBooks.Count == 0)
            {
                Console.WriteLine("Sorry, no books with this title");
            }
            else
            {
                Console.WriteLine($"Books by title '{title}':");

                foreach (Book book in titleBooks)
                {
                    Console.WriteLine($"Author: {book.Author} | Year: {book.ReleaseYear}");
                }
            }
        }

        public void ShowBooksByParametrAuthor()
        {
            Console.WriteLine("Enter the author:");
            string author = Console.ReadLine();
            List<Book> authorBooks = _books.FindAll(Book => Book.Author.ToLower() == author.ToLower());

            if (authorBooks.Count == 0)
            {
                Console.WriteLine("Sorry, no books with from this author");
            }
            else
            {
                Console.WriteLine($"Books by author '{author}':");

                foreach (Book book in authorBooks)
                {
                    Console.WriteLine($"Title: {book.Title} | Year: {book.ReleaseYear} ");
                }
            }
        }

        public void ShowBooksByParametrYear()
        {
            Console.WriteLine("Enter the release year:");
            int.TryParse(Console.ReadLine(), out int releaseYear);
            List<Book> releaseYearBooks = _books.FindAll(Book => Book.ReleaseYear == releaseYear);

            if (releaseYearBooks.Count == 0)
            {
                Console.WriteLine("Sorry, no books published int this year");
            }
            else
            {
                Console.WriteLine($"Books by release year '{releaseYear}':");

                foreach (Book book in releaseYearBooks)
                {
                    Console.WriteLine($"Title: {book.Title} | Author: {book.Author} ");
                }
            }
        }
    }
}
