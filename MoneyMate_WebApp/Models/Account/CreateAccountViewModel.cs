using System.ComponentModel.DataAnnotations;

namespace MoneyMate_WebApp.Models.Account
{
    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is required")]
        public Guid TypeId { get; set; }

        [Required(ErrorMessage = "Currency is required")]
        public Guid CurrencyId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Balance must be non-negative")]
        public decimal Balance { get; set; }
    }
}
