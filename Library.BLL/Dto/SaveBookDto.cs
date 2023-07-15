namespace Library.Backend.Models.Dto
{
    public class SaveBookDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
    }
}
