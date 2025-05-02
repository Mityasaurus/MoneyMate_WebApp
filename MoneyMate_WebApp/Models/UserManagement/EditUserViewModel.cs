using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MoneyMate_WebApp.Models.UserManagement
{
    public class EditUserViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(2)]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Roles")]
        public List<string> SelectedRoles { get; set; } = new();

        public List<SelectListItem> AvailableRoles { get; set; } = new();
    }
}
