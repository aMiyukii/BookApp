    using BookApp.Core.DTO;

    namespace BookApp.Models
    {
        public class AddBookViewModel
        {
            public List<BookDTO> Books { get; set; }
            
            public AddBookViewModel(List<BookDTO> books)
            {
                Books = books;
            }
        }
    }   