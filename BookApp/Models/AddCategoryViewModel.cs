using System.Collections.Generic;
using BookApp.Core.DTO;

namespace BookApp.Models
{
    public class AddCategoryViewModel
    {
        public List<CategoryDTO> Categories { get; set; }
        public string Name { get; set; }

        public AddCategoryViewModel()
        {
            Categories = new List<CategoryDTO>();
        }

        public AddCategoryViewModel(List<CategoryDTO> categories)
        {
            Categories = categories;
        }
    }
}