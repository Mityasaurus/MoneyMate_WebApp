using System.ComponentModel.DataAnnotations;

namespace MoneyMate_WebApp.Models.Account
{
    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "Введіть назву вашого рахунку")]
        [Display(Name = "Назва")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Оберіть тип зі списку")]
        [Display(Name = "Тип")]
        public Guid TypeId { get; set; }

        [Required(ErrorMessage = "Оберіть валюту зі списку")]
        [Display(Name = "Валюта")]
        public Guid CurrencyId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Початковий баланс повинен бути позитивним або нульовим")]
        [Display(Name = "Початковий Баланс")]
        public decimal Balance { get; set; }
    }
}
