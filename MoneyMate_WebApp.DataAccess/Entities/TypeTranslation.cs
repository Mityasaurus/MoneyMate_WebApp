namespace MoneyMate_WebApp.DataAccess.Entities
{
    public class TypeTranslation
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string LanguageCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public TypeEntity TypeEntity { get; set; } = null!;
    }
}
