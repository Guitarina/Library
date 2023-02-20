using System.Text.Json.Serialization;

namespace Library.Backend.Models.Domain
{
    public class Rating
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public int BookId { get; set; }

        //Navigation property
        [JsonIgnore]
        public Book Book { get; set; }
    }
}
