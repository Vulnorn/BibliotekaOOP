using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotekaOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shelf shelf = new Shelf();

            const string AddNewBookMenu = "1";
            const string ShowBooksMenu = "2";
            const string DeleteBookMenu = "3";
            const string ShowBooksAccordingAuthorMenu = "4";
            const string ShowBooksAccordingNameMenu = "5";
            const string ShowBooksAccordingYearPublicationMenu = "6";
            const string ExitMenu = "7";

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine($"Выберите пункт в меню:");
                Console.WriteLine($"{AddNewBookMenu} - Добавить новую книгу.");
                Console.WriteLine($"{ShowBooksMenu} - Показать все книги.");
                Console.WriteLine($"{DeleteBookMenu} - Удалить книгу");
                Console.WriteLine($"{ShowBooksAccordingAuthorMenu} - Показать все книги одного автора");
                Console.WriteLine($"{ShowBooksAccordingNameMenu} - Показать все книги с одинаковым названием");
                Console.WriteLine($"{ShowBooksAccordingYearPublicationMenu} - Показать все книги с одинаковым годом издания");
                Console.WriteLine($"{ExitMenu} - Выход");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case AddNewBookMenu:
                        shelf.AddBook();
                        break;

                    case ShowBooksMenu:
                        shelf.ShowAllBooks();
                        break;

                    case DeleteBookMenu:
                        shelf.DeleteBook();
                        break;

                    case ShowBooksAccordingAuthorMenu:
                        shelf.FindBooksAccordingAuthor();
                        break;

                    case ShowBooksAccordingNameMenu:
                        shelf.FindBooksAccordingName();
                        break;

                    case ShowBooksAccordingYearPublicationMenu:
                        shelf.FindBooksAccordingYearPublication();
                        break;

                    case ExitMenu:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Ошибка ввода команды.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

    class Book
    {
        public Book(string name, string author, int yearPublication)
        {
            Name = name;
            Author = author;
            YearPublication = yearPublication;
        }

        public string Name { get; private set; }
        public string Author { get; private set; }
        public int YearPublication { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"Название - {Name}: Автор - {Author}: Год издания -  {YearPublication}\n");
        }
    }

    class Shelf
    {
        private List<Book> _books = new List<Book>();

        public Shelf()
        {
            FillBooks();
        }

        public void AddBook()
        {
            Console.Clear();

            string name = InputUserString("Введите название книги");
            string author = InputUserString("Введите автора книги");

            if (GetInputUserYear(out int yearPublication) == true)
            {
                _books.Add(new Book(name, author, yearPublication));
                Console.WriteLine("Книга добавлена");
            }
            else
            {
                Console.WriteLine("Книга не добывлена!");
            }

            Console.ReadKey();
        }

        public void DeleteBook()
        {
            Console.WriteLine("Введите номер книги для удаления");

            if (GetBookAccordingId(out Book book) == true)
            {
                _books.Remove(book);
                Console.WriteLine("Книга с таким номером удалена");
            }
            else
            {
                Console.WriteLine("Нет такой книги");
            }

            Console.ReadKey();
        }

        public void ShowAllBooks()
        {
            Console.Clear();

            for (int i = 0; i < _books.Count(); i++)
            {
                Console.Write($"№ {i + 1}) ");
                _books[i].ShowInfo();
            }

            Console.ReadKey();
        }

        public void FindBooksAccordingYearPublication()
        {
            Console.WriteLine("Введите год издания");

            Stack<Book> books = GetAllBooksAccordingYearPublication();

            if (books.Count == 0)
                Console.WriteLine("Нет книг с таким годом издания.");
            else
                ShowBooksAccording(books);

            Console.ReadKey();
        }

        public void FindBooksAccordingName()
        {
            string name = InputUserString("Введите название книги");

            Stack<Book> books = GetAllBooksAccordingName(name);

            if (books.Count == 0)
                Console.WriteLine("Нет книг с таким названием");
            else
                ShowBooksAccording(books);

            Console.ReadKey();
        }

        public void FindBooksAccordingAuthor()
        {
            string author = InputUserString("Введите название книги");

            Stack<Book> books = GetAllBooksAccordingAutor(author);

            if (books.Count == 0)
                Console.WriteLine("Нет книг с таким Автором");
            else
                ShowBooksAccording(books);

            Console.ReadKey();
        }

        private void ShowBooksAccording(Stack<Book> books)
        {
            while (books.Count > 0)
            {
                books.Pop().ShowInfo();
            }

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
            if (input == string.Empty)
                return true;
            else
                return false;
        }

        private bool GetInputUserYear(out int yearPublication)
        {
            string userInput;

            do
            {
                Console.WriteLine("Введите Год издания книги");
                userInput = Console.ReadLine();
            }
            while (GetInputValue(userInput, out yearPublication));

            if (GetNumberRange(yearPublication))
            {
                Console.WriteLine("Год издания книги не может быть нулем или отрицательным числом");
                return false;
            }

            return true;
        }

        private bool GetInputValue(string input, out int number)
        {
            if (int.TryParse(input, out number) == false)
            {
                Console.WriteLine("Не корректный ввод.");
                return true;
            }

            return false;
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

            GetInputValue(userInput, out int number);
            number--;

            for (int i = 0; i < _books.Count; i++)
            {
                if (number == i)
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
                    if (year == _books[i].YearPublication)
                    {
                        books.Push(_books[i]);
                    }
                }
            }

            return books;
        }

        private Stack<Book> GetAllBooksAccordingName(string name)
        {
            Stack<Book> books = new Stack<Book>();

            for (int i = 0; i < _books.Count; i++)
            {
                if (name.ToLower() == _books[i].Name.ToLower())
                {
                    books.Push(_books[i]);
                }
            }

            return books;
        }

        private Stack<Book> GetAllBooksAccordingAutor(string author)
        {
            Stack<Book> books = new Stack<Book>();

            for (int i = 0; i < _books.Count; i++)
            {
                if (author.ToLower() == _books[i].Author.ToLower())
                {
                    books.Push(_books[i]);
                }
            }

            return books;
        }

        private void FillBooks()
        {
            _books.Add(new Book("1984", "Оруэлл Джордж", 1949));
            _books.Add(new Book("Убийство в «Восточном экспрессе»", "Агата Кристи", 1934));
            _books.Add(new Book("Меч Предназначения", "Анджей Сапковский", 1992));
            _books.Add(new Book("Тёмная сторона", "Макс Фрай", 1997));
            _books.Add(new Book("Сундук мертвеца", "Макс Фрай", 2017));
            _books.Add(new Book("Отдай мое сердце", "Макс Фрай", 2017));
        }
    }
}