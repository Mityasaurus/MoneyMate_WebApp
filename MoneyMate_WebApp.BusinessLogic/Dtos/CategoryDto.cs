using MoneyMate_WebApp.DataAccess.Entities;

namespace MoneyMate_WebApp.BusinessLogic.Dtos
{
    public class CategoryDto(Guid id, string name, TransactionType type) : IEquatable<CategoryDto>
    {
        public static readonly CategoryDto Default = new(Guid.Empty, string.Empty, TransactionType.Expense);

        public Guid Id { get; } = id;
        public string Name { get; } = name;
        public TransactionType Type { get; } = type;
        public bool Equals(CategoryDto? other)
        {
            if (other is null) return false;
            return Id == other.Id && Name == other.Name && Type == other.Type;
        }
        public override bool Equals(object? obj) => Equals(obj as CategoryDto);
        public override int GetHashCode() => HashCode.Combine(Id, Name, Type);
    }
}
