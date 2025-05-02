using System.ComponentModel.DataAnnotations;

namespace MoneyMate_WebApp.Models.UserAccount
{
    public class EditProfileViewModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(2)]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;
    }
}
