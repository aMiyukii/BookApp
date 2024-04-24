﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStandard { get; set; }

        public Category() { }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}