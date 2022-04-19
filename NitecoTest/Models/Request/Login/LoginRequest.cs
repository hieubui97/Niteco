using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NitecoTest.Models.Request
{
    public class LoginRequest
    {
        [Required]
        //[RegularExpression("/^(([^<>()[\\]\\\\.,;:\\s@\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$/")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
