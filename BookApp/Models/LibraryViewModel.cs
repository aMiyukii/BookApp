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
    }
}