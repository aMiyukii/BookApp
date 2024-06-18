using BookApp.Core.DTO;

namespace BookApp.Models
{
    public class LibraryViewModel
    {
        public List<BookViewModel> Books { get; set; } = new List<BookViewModel>();
        public List<CategoryDTO> CategoriyDTO { get; set; }

        public LibraryViewModel() { }

        public LibraryViewModel(List<CategoryDTO> categoriyDTO)
        {
            CategoriyDTO = categoriyDTO;
        }
    }

    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Serie { get; set; }
        public string Genre { get; set; }
        public string ImageUrl { get; set; }
        
        public int UserId { get; set; }
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<CategoryViewModel> AllCategories { get; set; } = new List<CategoryViewModel>();

        public class CategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsStandard { get; set; }
        }
    }
}