namespace MoneyMate_WebApp.DataAccess.Entities
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public ICollection<CurrencyTranslation> Translations { get; set; } = [];
    }
}
