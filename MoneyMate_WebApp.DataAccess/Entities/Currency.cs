namespace MoneyMate_WebApp.DataAccess.Entities
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string Symbol { get; set; }
    }
}
