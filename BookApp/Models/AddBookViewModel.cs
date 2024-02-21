namespace BookApp.Models
{
    public class AddBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string categoryId { get; set; }

        public AddBookViewModel()
        {

        }

        public AddBookViewModel(string title)
        {
            Title = title;
        }
    }
}