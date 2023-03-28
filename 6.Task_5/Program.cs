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

            const int FirstButton = 1;
            const int SecondButton = 2;
            const int ThirdButton = 3;
            const int FourthButton = 4;
            const int FifthButton = 5;

            while (isWork)
            {
                Console.WriteLine(FirstButton + " - add book");
                Console.WriteLine(SecondButton + " - show all book in storage");
                Console.WriteLine(ThirdButton + " - remove book");
                Console.WriteLine(FourthButton + " - show book by parametr");
                Console.WriteLine(FifthButton + " - EXIT");

                int.TryParse(Console.ReadLine(), out int key);

                switch (key)
                {
                    case FirstButton:
                        storage.AddBook();
                        break;
                    case SecondButton:
                        storage.ShowAllBooks();
                        break;
                    case ThirdButton:
                        storage.RemoveBook();
                        break;
                    case FourthButton:
                        storage.ShowBooksByParametr();
                        break;
                    case FifthButton:
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
            int releaseYear = Convert.ToInt32(Console.ReadLine());
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
            Console.WriteLine("For remove enter book title:");
            string title = Console.ReadLine();
            Console.WriteLine("For remove enter book author:");
            string author = Console.ReadLine();
            int index=_books.FindIndex(Book => Book.Title.ToLower() == title.ToLower() && Book.Author.ToLower() == author.ToLower());
            _books.RemoveAt(index);            
        }

        public void ShowBooksByParametr()
        {
            const int FirstButton = 1;
            const int SecondButton = 2;
            const int ThirdButton = 3;

            Console.WriteLine($"Push {FirstButton} to search book for title.");
            Console.WriteLine($"Push {SecondButton} to search book for author.");
            Console.WriteLine($"Push {ThirdButton} to search book for relese year.");
            int.TryParse(Console.ReadLine(), out int key);

            switch (key)
            {
                case FirstButton:
                    ShowBooksByParametrTitle();
                    break;
                case SecondButton:
                    ShowBooksByParametrAuthor();
                    break;
                case ThirdButton:
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
