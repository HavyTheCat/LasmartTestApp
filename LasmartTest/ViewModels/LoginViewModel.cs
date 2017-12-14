using System.ComponentModel.DataAnnotations;


namespace LasmartTest.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
