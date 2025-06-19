using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealWell.Models
{
    public class RegisterModel
    {
        // [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        // [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        // [Required(ErrorMessage = "Email is required")]
        // [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        // [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; } = string.Empty;

        // [Required(ErrorMessage = "Password is required")]
        // [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = string.Empty;

        // [Required(ErrorMessage = "Please confirm your password")]
        // [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        // [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms")]
        public bool AcceptTerms { get; set; }
    }
}
