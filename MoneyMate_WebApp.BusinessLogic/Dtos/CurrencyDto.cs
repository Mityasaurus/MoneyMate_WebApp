namespace MoneyMate_WebApp.BusinessLogic.Dtos
{
    public class CurrencyDto(string currencyName, string currencyCode, string currencySymbol) : IEquatable<CurrencyDto>
    {
        public static readonly CurrencyDto Default = new(string.Empty, string.Empty, string.Empty);
        public string CurrencyName { get; } = currencyName;
        public string CurrencyCode { get; } = currencyCode;
        public string CurrencySymbol { get; } = currencySymbol;
        public bool Equals(CurrencyDto? other)
        {
            if (other is null) return false;
            return CurrencyName == other.CurrencyName && CurrencyCode == other.CurrencyCode && CurrencySymbol == other.CurrencySymbol;
        }
        public override bool Equals(object? obj) => Equals(obj as CurrencyDto);
        public override int GetHashCode() => HashCode.Combine(CurrencyName, CurrencyCode, CurrencySymbol);
    }
}
