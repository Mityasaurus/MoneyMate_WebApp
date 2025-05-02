using Microsoft.AspNetCore.Identity;

namespace MoneyMate_WebApp.DataAccess.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
