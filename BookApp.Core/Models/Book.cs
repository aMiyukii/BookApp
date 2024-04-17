namespace BookApp.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Serie { get; set; }
        public string Image { get; set; }

        //methodes bijdoen (gedrag)

        public Book()
        {

        }

    }
}