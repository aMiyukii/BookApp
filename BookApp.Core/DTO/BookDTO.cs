using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.Core.Models;
using BookApp.Data;

namespace BookApp.Core.DTO
{
    public class BookDTO
    {
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            List<BookRepository> bookDataList = BookRepository.GetBooksFromDatabase();

            foreach (var bookData in bookDataList)
            {
                Book book = new Book
                {
                    Id = bookData.Id,
                    Title = bookData.Title,
                    Author = bookData.Author,
                    Image = bookData.Image,
                };

                books.Add(book);
            }

            return books;
        }



    }
}

