using System.ComponentModel.DataAnnotations;

namespace universityAPI.Models.DataModels
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
