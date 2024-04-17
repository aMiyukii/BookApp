using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.Core.Models;

namespace BookApp.Core.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Serie { get; set; }
        public string Image { get; set; }

        public BookDTO() { }

        public BookDTO(int id, string title, string author, string image) 
        {
            Id = id;
            Title = title;
            Author = author;
            Image = image;

        }


        //dto of domein
        //getallbooks naamgeving

        //public List<Book> GetAllBooks()
        //{
        //    List<Book> books = new List<Book>();

        //    List<BookRepository> bookDataList = BookRepository.GetBooksFromDatabase();

        //    foreach (var bookData in bookDataList)
        //    {
        //        Book book = new Book
        //        {
        //            Id = bookData.Id,
        //            Title = bookData.Title,
        //            Author = bookData.Author,
        //            Image = bookData.Image,
        //        };

        //        books.Add(book);
        //    }

        //    return books;
        //}
    }
}

