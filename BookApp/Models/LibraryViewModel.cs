namespace BookApp.Models
{
    public class LibraryViewModel
    {
        public List<BookViewModel> Books { get; set; }
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Serie { get; set; }
        public string Genre { get; set; }
        public string ImageUrl { get; set; }
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
    
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStandard { get; set; }
    }

}