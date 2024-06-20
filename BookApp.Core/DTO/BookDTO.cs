namespace BookApp.Core.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Serie { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }

        public BookDTO()
        {
        }

        public BookDTO(int id, string title, string author, string imageurl)
        {
            Id = id;
            Title = title;
            Author = author;
            ImageUrl = imageurl;
        }

        public BookDTO(int id, string title, string author, string imageurl, string serie, string genre)
        {
            Id = id;
            Title = title;
            Author = author;
            ImageUrl = imageurl;
            Serie = serie;
            Genre = genre;
        }
    }
}