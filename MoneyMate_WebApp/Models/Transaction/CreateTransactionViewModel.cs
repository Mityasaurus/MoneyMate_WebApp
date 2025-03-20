using MoneyMate_WebApp.BusinessLogic.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MoneyMate_WebApp.Models.Transaction
{
    public class CreateTransactionViewModel
    {
        [Required]
        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "Оберіть категорію зі списку")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Вкажіть дату проведення операції")]
        [NotFutureDate]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Введіть суму операції")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма повинна бути більшою за нуль")]
        public decimal Amount { get; set; }

        public string? Comment { get; set; } = string.Empty;
    }
}
