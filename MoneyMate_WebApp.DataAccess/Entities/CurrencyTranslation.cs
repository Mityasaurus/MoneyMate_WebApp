namespace MoneyMate_WebApp.DataAccess.Entities
{
    public class CurrencyTranslation
    {
        public Guid Id { get; set; }
        public Guid CurrencyId { get; set; }
        public string LanguageCode { get; set; } = string.Empty;
        public string CurrencyName { get; set; } = string.Empty;
        public Currency Currency { get; set; } = null!;
    }
}
