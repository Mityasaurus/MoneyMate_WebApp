namespace MoneyMate_WebApp.DataAccess.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>();
    }
}
