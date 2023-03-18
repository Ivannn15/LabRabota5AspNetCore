using System;

namespace Lr5.Models
{
    public class Book
    {
        public int BookId { get; set; } // Первичный ключ для таблицы Books

        public string Title { get; set; } // Поля таблицы
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public int AuthorId { get; set; }


        public Author Author { get; set; } // Связанный по внешнему ключу экземпляр класса Author

        ////// Таблица Books
    }
}
