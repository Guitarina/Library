using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Library.Backend.Models.Dto
{
    public class RateBookDto
    {
        [Range(1,5)]
        public int Score { get; set; }
    }
}
