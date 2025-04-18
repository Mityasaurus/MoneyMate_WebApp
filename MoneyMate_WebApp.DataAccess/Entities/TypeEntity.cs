namespace MoneyMate_WebApp.DataAccess.Entities
{
    public class TypeEntity
    {
        public Guid Id { get; set; }
        public ICollection<TypeTranslation> Translations { get; set; } = [];
    }
}
