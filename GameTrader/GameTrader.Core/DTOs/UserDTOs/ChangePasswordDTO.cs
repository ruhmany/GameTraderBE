using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GameTrader.Core.DTOs.UserDTOs
{
    public class ChangePasswordDTO
    {
        public string? Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Old password is required")]
        public string OldPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "New password is required")]
        public string NewPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Repeat new password is required")]
        [Compare(nameof(NewPassword), ErrorMessage = "New password and confirm password must match")]
        public string RepeatNewPassword { get; set; }


        public string Validate()
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            if (NewPassword.Length < 8) return "Password must be at least 8 characters";
            if (!NewPassword.Any(c => char.IsUpper(c))) return "Password must contain at least one uppercase letter";
            if (!NewPassword.Any(c => char.IsLower(c))) return "Password must contain at least one lowercase letter";
            if (!NewPassword.Any(c => char.IsDigit(c))) return "Password must contain at least one number";
            if (!NewPassword.Any(c => !char.IsLetterOrDigit(c))) return "Password must include at least one special character";
            if (!regex.IsMatch(NewPassword)) return "Invalid password";
            return string.Empty;
        }
    }
}
