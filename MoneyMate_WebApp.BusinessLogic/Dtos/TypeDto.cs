namespace MoneyMate_WebApp.BusinessLogic.Dtos
{
    public class TypeDto(Guid id, string name) : IEquatable<TypeDto>
    {
        public static readonly TypeDto Default = new(Guid.Empty, string.Empty);

        public Guid Id { get; } = id;
        public string Name { get; } = name;
        public bool Equals(TypeDto? other)
        {
            if (other is null) return false;
            return Id == other.Id && Name == other.Name;
        }
        public override bool Equals(object? obj) => Equals(obj as TypeDto);
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
