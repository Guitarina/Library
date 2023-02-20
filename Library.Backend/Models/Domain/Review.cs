﻿using System.Text.Json.Serialization;

namespace Library.Backend.Models.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Reviewer { get; set; }
        public int BookId { get; set; }

        //Navigation property
        [JsonIgnore]
        public Book Book { get; set; }
    }
}
