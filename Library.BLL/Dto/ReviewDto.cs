using Library.Backend.Models.Domain;

namespace Library.Backend.Models.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Reviewer { get; set; }

    }
}
