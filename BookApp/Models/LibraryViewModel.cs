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
    }
}