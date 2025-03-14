namespace MoneyMate_WebApp.BusinessLogic.Dtos
{
    public class AccountDto(string name, Guid typeId, Guid currencyId, decimal balance) : IEquatable<AccountDto>
    {
        public static readonly AccountDto Default = new(string.Empty, Guid.Empty, Guid.Empty, 0m);
        public string Name { get; } = name;
        public Guid TypeId { get; } = typeId;
        public Guid CurrencyId { get; } = currencyId;
        public decimal Balance { get; } = balance;
        public bool Equals(AccountDto? other)
        {
            if (other is null) return false;
            return Name == other.Name && TypeId == other.TypeId && CurrencyId == other.CurrencyId && Balance == other.Balance;
        }
        public override bool Equals(object? obj) => Equals(obj as AccountDto);
        public override int GetHashCode() => HashCode.Combine(Name, TypeId, CurrencyId, Balance);
    }
}
