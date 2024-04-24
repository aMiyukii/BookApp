using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStandard { get; set; }

        public CategoryDTO() { }

        // Constructor with parameters
        public CategoryDTO(int id, string name, bool isStandard)
        {
            Id = id;
            Name = name;
            IsStandard = isStandard;
        }
    }
}

