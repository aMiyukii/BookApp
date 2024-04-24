using BookApp.Core.DTO;

namespace BookApp.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryDTO> Categories { get; set; }

        public CategoryViewModel() { }


        public CategoryViewModel(string name) { Name = name; }
    }
}
