using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.Task_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();

            bool isWork = true;

            while (isWork)
            {
                int key;
                Console.WriteLine("1. - add book");
                Console.WriteLine("2. - show all book in storage");
                Console.WriteLine("3. - remove book");
                Console.WriteLine("4. - show book by parametr");
                Console.WriteLine("5. - EXIT");

                key = Convert.ToInt32(Console.ReadLine());

                switch (key)
                {
                    case 1:
                        {
                            storage.AddBook();
                            break;
                        }
                    case 2:
                        {
                            storage.ShowAllBooks();
                            break;
                        }
                    case 3:
                        {
                            storage.RemoveBook();
                            break;
                        }
                    case 4:
                        {
                            storage.ShowBooksByParametr();
                            break;
                        }
                    case 5:
                        {
                            isWork=false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Input correct number!");
                            break;
                        }
                }
            }
        }
    }

    class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int ReleaseYear { get; private set; }

        public Book(string title, string author, int releaseYear)
        {
            Title = title;
            Author = author;
            ReleaseYear = releaseYear;
        }
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
                Console.WriteLine("Title: {0} | Author: {1} | Year: {2}", book.Title, book.Author, book.ReleaseYear);
            }
        }

        public void RemoveBook()
        {
            Console.WriteLine("For remove enter book title:");
            string title = Console.ReadLine();
            Console.WriteLine("For remove enter book author:");
            string author = Console.ReadLine();
            _books.RemoveAll(Book => Book.Title == title && Book.Author == author);
        }

        public void ShowBooksByParametr()
        {
            Console.WriteLine("Push 1 to search book for title.");
            Console.WriteLine("Push 2 to search book for author.");
            Console.WriteLine("Push 3 to search book for relese year.");
            int key = Convert.ToInt32(Console.ReadLine());

            switch (key)
            {
                case 1:
                    {
                        Console.WriteLine("Enter the title:");
                        string title = Console.ReadLine();
                        List<Book> titleBooks = _books.FindAll(Book => Book.Title == title);

                        if (titleBooks.Count == 0)
                        {
                            Console.WriteLine("Sorry, no books with this title");
                        }
                        else
                        {
                            Console.WriteLine("Books by title'{0}':", title);

                            foreach (Book book in titleBooks)
                            {
                                Console.WriteLine("Author: {0} | Year: {1}", book.Author, book.ReleaseYear);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter the author:");
                        string author = Console.ReadLine();
                        List<Book> authorBooks = _books.FindAll(Book => Book.Author == author);

                        if (authorBooks.Count == 0)
                        {
                            Console.WriteLine("Sorry, no books with from this author");
                        }
                        else
                        {
                            Console.WriteLine("Books by author '{0}':", author);

                            foreach (Book book in authorBooks)
                            {
                                Console.WriteLine("Title: {0} | Year: {1} ", book.Title, book.ReleaseYear);
                            }
                        }

                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Enter the release year:");
                        int releaseYear = Convert.ToInt32(Console.ReadLine());
                        List<Book> releaseYearBooks = _books.FindAll(Book => Book.ReleaseYear == releaseYear);

                        if (releaseYearBooks.Count == 0)
                        {
                            Console.WriteLine("Sorry, no books published int this year");
                        }
                        else
                        {
                            Console.WriteLine("Books by release year '{0}':", releaseYear);

                            foreach (Book book in releaseYearBooks)
                            {
                                Console.WriteLine("Title: {0} | Author: {1} ", book.Title, book.Author);
                            }
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Input correct number!");
                        break;
                    }
            }
        }
    }
}
