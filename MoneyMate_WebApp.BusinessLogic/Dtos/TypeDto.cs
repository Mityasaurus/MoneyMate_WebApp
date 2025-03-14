namespace MoneyMate_WebApp.BusinessLogic.Dtos
{
    public class TypeDto(string name) : IEquatable<TypeDto>
    {
        public static readonly TypeDto Default = new(string.Empty);
        public string Name { get; } = name;
        public bool Equals(TypeDto? other)
        {
            if (other is null) return false;
            return Name == other.Name;
        }
        public override bool Equals(object? obj) => Equals(obj as TypeDto);
        public override int GetHashCode() => Name.GetHashCode();
    }
}
