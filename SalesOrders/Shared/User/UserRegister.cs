using System.ComponentModel.DataAnnotations;

namespace SalesOrders.Shared.User
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string email { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 2)]
        public string password { get; set; } = string.Empty;

        [Compare("password", ErrorMessage = "The passwords do not match....")]
        public string confirmPassword { get; set; } = string.Empty;
    }
}
