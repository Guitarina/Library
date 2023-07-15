using Library.Backend.Models.Domain;

namespace Library.Backend.Models.Dto
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }

        public double Rating { get; set; }

        public IEnumerable<ReviewDto> Reviews { get; set; }

    }
}
