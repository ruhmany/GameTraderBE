using GameTrader.Core.Enums;
using GameTrader.Core.StaticData;
using System.ComponentModel.DataAnnotations;

namespace GameTrader.API.Models
{
    public class AddUserModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.FirstNameIsRequired)]
        [MaxLength(50, ErrorMessage = ValidationMessages.MaxLength)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = ValidationMessages.LettersOnly)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.LastNameIsRequired)]
        [MaxLength(50, ErrorMessage = ValidationMessages.MaxLength)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = ValidationMessages.LettersOnly)]
        public string LastName { get; set; }

        [Required(ErrorMessage = ValidationMessages.EmailIsRequired)]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = ValidationMessages.EmailNotValid)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.PasswordIsRequired)]
        [MinLength(8, ErrorMessage = ValidationMessages.PasswordMinLength)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]", ErrorMessage = ValidationMessages.PasswordComplexity)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessages.PhoneNumberIsRequired)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = ValidationMessages.UserRoleIsRequired)]
        [EnumDataType(typeof(RoleEnum), ErrorMessage = ValidationMessages.InvalidRole)]
        public RoleEnum? Role { get; set; }

        [Required(ErrorMessage = ValidationMessages.StatusIsRequired)]
        public bool Status { get; set; }
        public string? ImageId { get; set; }
    }
}
