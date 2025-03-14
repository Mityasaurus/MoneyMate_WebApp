namespace MoneyMate_WebApp.DataAccess.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TypeId { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

        public TypeEntity Type { get; set; }
        public Currency Currency { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
