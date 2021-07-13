using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models
{
    public class CustomIdentityRequestModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
