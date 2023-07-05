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

        }
    }

    class Book
    {
        public Book(string name, string author, string yearRelease )
        {
            Name = name;
            Author = author;
            YearRelease = yearRelease;
        }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public string YearRelease { get; private set; }

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

            string name = InputUser( "Введите название книги");
            string author = InputUser( "Введите автора книги");
            string yearRelease = InputUser( "Введите Год издания книги");

            _books.Add(new Book(name, author, yearRelease));
        }

       public void DeleteBook()
        {
            if (TryGetBook(out Book book))
            {
                _books.Remove( book);
                Console.WriteLine("Книга с таким номером удалена");
                Console.ReadKey();
            }
        }

        public void Show ()
        {
            Console.Clear();
            for (int i=0;i<_books.Count();i++)
            {
                Console.Write($"№ { i + 1}) ");
                _books[i].ShowInfo();
            }
        }
        private string InputUser( string massage)
        {

            string userInput = null;

            while (VerifyInput(userInput))
            {
                Console.WriteLine(massage);
                userInput = Console.ReadLine();

                if (userInput == null)
                    Console.WriteLine("Поле не может быть пустым");
            }

            return userInput;
        }

        private bool VerifyInput(string input)
        {
            if (input == null)
                return true;
            else
                return false;
        }

        private bool TryGetBook(out Book book)
        {
            int id;
            book = null;

            Console.WriteLine("Введите номер книги для удаления");
            string userInput = Console.ReadLine().Trim();

            if (int.TryParse(userInput, out id) == false)
            {
                Console.WriteLine("Не корректный ввод.");
                Console.ReadKey();
                return false;
            }

            id--;

            for (int i = 0; i < _books.Count; i++)
            {
                if (id == i)
                {
                    book = _books[i];
                    return true;
                }
            }

            Console.WriteLine("Нет такой книги");
            Console.ReadKey();
            return false;
        }


    }
}
