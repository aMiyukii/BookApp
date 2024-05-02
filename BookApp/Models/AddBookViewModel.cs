    using BookApp.Core.DTO;

    namespace BookApp.Models
    {
        public class AddBookViewModel
        {
            public List<BookDTO> Books { get; set; }
            public List<CategoryDTO> Categories { get; set; }

            public AddBookViewModel(List<BookDTO> books)
            {
                Books = books;
            }

            public AddBookViewModel(List<BookDTO> books, List<CategoryDTO> categories)
            {
                Books = books;
                Categories = categories;
            }
        }
    }   