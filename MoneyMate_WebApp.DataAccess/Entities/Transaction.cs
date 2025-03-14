namespace MoneyMate_WebApp.DataAccess.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Навігаційні властивості
        public Account Account { get; set; }
        public Category Category { get; set; }
    }
}
