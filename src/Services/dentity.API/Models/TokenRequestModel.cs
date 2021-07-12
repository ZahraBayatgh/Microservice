using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models
{
    public class TokenRequestModel
    {
        [Required]
        [MinLength(5)]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
