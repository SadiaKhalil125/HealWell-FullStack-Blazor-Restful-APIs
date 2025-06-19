namespace HealWell.Models
{
    public class ResetPasswordModel
    {

        public string Email { get; set; }

        // [Required(ErrorMessage = "Password is required")]
        // [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

    }
}
