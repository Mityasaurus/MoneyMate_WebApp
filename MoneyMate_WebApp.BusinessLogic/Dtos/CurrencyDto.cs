namespace MoneyMate_WebApp.BusinessLogic.Dtos
{
    public class CurrencyDto(Guid id, string currencyName, string currencyCode, string currencySymbol) : IEquatable<CurrencyDto>
    {
        public static readonly CurrencyDto Default = new(Guid.Empty, string.Empty, string.Empty, string.Empty);

        public Guid Id { get; } = id;
        public string CurrencyName { get; } = currencyName;
        public string CurrencyCode { get; } = currencyCode;
        public string CurrencySymbol { get; } = currencySymbol;
        public bool Equals(CurrencyDto? other)
        {
            if (other is null) return false;
            return Id == other.Id && CurrencyName == other.CurrencyName && CurrencyCode == other.CurrencyCode && CurrencySymbol == other.CurrencySymbol;
        }
        public override bool Equals(object? obj) => Equals(obj as CurrencyDto);
        public override int GetHashCode() => HashCode.Combine(Id, CurrencyName, CurrencyCode, CurrencySymbol);
    }
}
