namespace MoneyMate_WebApp.Models.Account
{
    public class AccountDisplayViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AccountTypeName { get; set; } = string.Empty;
        public string CurrencyName { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = string.Empty;
        public string CurrencySymbol { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
