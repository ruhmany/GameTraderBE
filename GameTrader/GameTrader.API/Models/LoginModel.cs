using GameTrader.Core.Enums;
using GameTrader.Core.StaticData;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameTrader.API.Models
{
    public class LoginModel : IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var userManager = validationContext.GetService<UserManager<global::GameTrader.Data.DomainModels.User>>();
            var roleManager = validationContext.GetService<RoleManager<global::GameTrader.Data.DomainModels.Role>>();
            var context = validationContext.GetService<IHttpContextAccessor>();
            var user = Task.Run(async () => await userManager.FindByEmailAsync(Email)).Result;
            if (user == null)
            {
                yield return new ValidationResult(ValidationMessages.InvalidEmailOrPassword, new List<string> { nameof(Email) });
                yield break;
            }
            var userRole = Task.Run(async () => await userManager.GetRolesAsync(user)).Result;       
        }
    }
}
