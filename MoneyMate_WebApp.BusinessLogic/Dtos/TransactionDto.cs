namespace MoneyMate_WebApp.BusinessLogic.Dtos
{
    public class TransactionDto(Guid id, Guid accountId, Guid categoryId, DateTime date, decimal amount, string? comment, AccountDto? account = null, CategoryDto? category = null) : IEquatable<TransactionDto>
    {
        public static readonly TransactionDto Default = new(Guid.Empty, Guid.Empty, Guid.Empty, DateTime.MinValue, 0m, null, AccountDto.Default, CategoryDto.Default);

        public Guid Id { get; } = id;
        public Guid AccountId { get; } = accountId;
        public Guid CategoryId { get; } = categoryId;
        public DateTime Date { get; } = date;
        public decimal Amount { get; } = amount;
        public string? Comment { get; } = comment;
        public AccountDto? Account { get; } = account;
        public CategoryDto? Category { get; } = category;
        public bool Equals(TransactionDto? other)
        {
            if (other is null) return false;
            return AccountId == other.AccountId &&
                   CategoryId == other.CategoryId &&
                   Date == other.Date &&
                   Amount == other.Amount &&
                   Comment == other.Comment &&
                   Equals(Account, other.Account) &&
                   Equals(Category, other.Category);
        }
        public override bool Equals(object? obj) => Equals(obj as TransactionDto);
        public override int GetHashCode() => HashCode.Combine(AccountId, CategoryId, Date, Amount, Comment, Account, Category);
    }
}
