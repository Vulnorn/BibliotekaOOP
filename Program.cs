using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shelf shelf = new Shelf();
            shelf.AddBook();
            shelf.ShowAllBooks();
            Console.ReadKey();
        }
    }

    class Book
    {
        public Book(string name, string author, int yearRelease)
        {
            Name = name;
            Author = author;
            YearRelease = yearRelease;
        }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public int YearRelease { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"Название - {Name}: Автор - {Author}: Год выпуска -  {YearRelease} ");
        }
    }

    class Shelf
    {
        private List<Book> _books = new List<Book>();

        public string NumberBooks { get; private set; }

        public void AddBook()
        {
            Console.Clear();

            string name = InputUserString("Введите название книги");
            string author = InputUserString("Введите автора книги");

            if (GetInputUserYear(out int yearPublication) == true)
                _books.Add(new Book(name, author, yearPublication));
            else
                Console.WriteLine("Книга не добывлена!");

        }

        public void DeleteBook()
        {
            Console.WriteLine("Введите номер книги для удаления");

            if (GetBookAccordingId(out Book book) == true)
            {
                _books.Remove(book);
                Console.WriteLine("Книга с таким номером удалена");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Нет такой книги");
                Console.ReadKey();
            }
        }

        public void ShowAllBooks()
        {
            Console.Clear();
            for (int i = 0; i < _books.Count(); i++)
            {
                Console.Write($"№ {i + 1}) ");
                _books[i].ShowInfo();
            }
        }

        public void ShowBooksAccordingYearPublication()
        {
            Console.WriteLine("Введите год издания");

            Stack<Book> books = GetAllBooksAccordingYearPublication();

            if(books.Count==0)
                Console.WriteLine("Нет книг с таким годом издания.");

               books.Pop().ShowInfo();
        }

        private string InputUserString(string massage)
        {
            string userInput;

            do
            {
                Console.WriteLine(massage);
                userInput = Console.ReadLine();

                if (GetInputString(userInput) == true)
                    Console.WriteLine("Поле не может быть пустым");
            }
            while (GetInputString(userInput));

            return userInput;
        }

        private bool GetInputString(string input)
        {
            if (input == null)
                return true;
            else
                return false;
        }

        private bool GetInputUserYear(out int yearPublication)
        {
            Console.WriteLine("Введите Год издания книги");
            string userInput = Console.ReadLine();

            if (GetInputValue(userInput, out yearPublication) == false)
                return false;

            if (GetNumberRange(yearPublication))
            {
                Console.WriteLine("Год издания книги не может быть нулем или отрицательным числом");
                Console.ReadKey();
                return false;
            }

            return true;
        }

        private bool GetInputValue(string input, out int number)
        {
            if (int.TryParse(input, out number) == false)
            {
                Console.WriteLine("Не корректный ввод.");
                Console.ReadKey();
                return false;
            }

            return true;
        }

        private bool GetNumberRange(int number)
        {
            int minimunYear = 1;

            if (number < minimunYear)
                return true;

            return false;
        }

        private bool GetBookAccordingId(out Book book)
        {
            book = null;

            string userInput = Console.ReadLine();

            if (GetInputValue(userInput, out int id) == false)
                return false;

            id--;

            for (int i = 0; i < _books.Count; i++)
            {
                if (id == i)
                {
                    book = _books[i];
                    return true;
                }
            }

            return false;
        }

        private Stack<Book> GetAllBooksAccordingYearPublication()
        {
            Stack<Book> books = new Stack<Book>();

            string userInput = Console.ReadLine();

            if (GetInputValue(userInput, out int year) == false)
            {
                for (int i = 0; i < _books.Count; i++)
                {
                    if (year == _books[i].YearRelease)
                    {
                        books.Push(_books[i]);
                    }
                }
            }

            return books;
        }
    }
}