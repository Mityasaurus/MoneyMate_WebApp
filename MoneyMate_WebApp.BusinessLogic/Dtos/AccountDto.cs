namespace MoneyMate_WebApp.BusinessLogic.Dtos
{
    public class AccountDto(Guid id, string name, Guid typeId, Guid currencyId, decimal balance, TypeDto? type = null, CurrencyDto? currency = null) : IEquatable<AccountDto>
    {
        public static readonly AccountDto Default = new(Guid.Empty, string.Empty, Guid.Empty, Guid.Empty, 0m, TypeDto.Default, CurrencyDto.Default);
        
        public Guid Id { get; } = id;
        public string Name { get; } = name;
        public Guid TypeId { get; } = typeId;
        public Guid CurrencyId { get; } = currencyId;
        public decimal Balance { get; } = balance;
        public TypeDto? Type { get; } = type;
        public CurrencyDto? Currency { get; } = currency;
        public bool Equals(AccountDto? other)
        {
            if (other is null) return false;
            return Name == other.Name &&
                   TypeId == other.TypeId &&
                   CurrencyId == other.CurrencyId &&
                   Balance == other.Balance &&
                   Type.Equals(other.Type) &&
                   Currency.Equals(other.Currency);
        }
        public override bool Equals(object? obj) => Equals(obj as AccountDto);
        public override int GetHashCode() => HashCode.Combine(Name, TypeId, CurrencyId, Balance, Type, Currency);
    }
}
