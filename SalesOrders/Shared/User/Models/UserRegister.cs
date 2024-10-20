using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.Users.Models
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string email { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 2)]
        public string password { get; set; } = string.Empty;

        [Required, Compare("password", ErrorMessage = "The passwords do not match....")]
        public string confirmPassword { get; set; } = string.Empty;

        public string? role { get; set; } = string.Empty;
    }
}
