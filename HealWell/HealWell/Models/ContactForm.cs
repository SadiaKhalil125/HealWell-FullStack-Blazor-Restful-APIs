using System.ComponentModel.DataAnnotations;

namespace HealWell.Models
{
    public class ContactForm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [MinLength(10, ErrorMessage = "Message must be at least 10 characters")]
        public string Message { get; set; }
    }
}
