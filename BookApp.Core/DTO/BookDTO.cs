﻿using System;
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
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Serie { get; set; }
        public string Image { get; set; }

        public BookDTO() { }

        public BookDTO(int id) { Id = id; }

        public BookDTO(int id, string title, string author, string image)
        {
            Id = id;
            Title = title;
            Author = author;
            Image = image;
        }
    }
}

