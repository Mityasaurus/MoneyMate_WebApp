using MoneyMate_WebApp.DataAccess.Entities;

namespace MoneyMate_WebApp.Models.Transaction
{
    public class TransactionDisplayViewModel
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public string CategoryName { get; set; } = "";
        public TransactionType Type { get; set; }
    }
}
