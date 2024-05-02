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
        public string ImageUrl { get; set; }

        public BookDTO() { }

        public BookDTO(int id, string title, string author, string imageUrl) 
        {
            Id = id;
            Title = title;
            Author = author;
            ImageUrl = imageUrl;
        }
        
        public BookDTO(int id, string title, string author, string imageUrl, string serie, string genre)
        {
            Id = id;
            Title = title;
            Author = author;
            ImageUrl = imageUrl;
            Serie = serie;
            Genre = genre;
        }
    }
}

