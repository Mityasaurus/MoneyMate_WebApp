using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneyMate_WebApp.DataAccess.Entities;

namespace MoneyMate_WebApp.DataAccess
{
    public class MoneyMateContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public MoneyMateContext() { }

        public MoneyMateContext(DbContextOptions<MoneyMateContext> options)
            : base(options)
        {
        }

        public DbSet<TypeEntity> Types { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TypeTranslation> TypeTranslations { get; set; }
        public DbSet<CurrencyTranslation> CurrencyTranslations { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TypeEntity>(entity =>
            {
                entity.ToTable("Types");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currencies");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CurrencyCode)
                      .HasColumnType("nvarchar(10)")
                      .HasMaxLength(10)
                      .IsRequired();
                entity.Property(e => e.Symbol)
                      .HasColumnType("nvarchar(10)")
                      .HasMaxLength(10)
                      .IsRequired();
                entity.HasIndex(e => e.CurrencyCode)
                      .IsUnique();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.Type)
                      .HasColumnType("nvarchar(20)")
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.Name)
                      .HasColumnType("nvarchar(100)")
                      .HasMaxLength(100)
                      .IsRequired();
                entity.Property(e => e.Balance)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()")
                      .IsRequired();

                entity.HasOne(a => a.Type)
                      .WithMany()
                      .HasForeignKey(a => a.TypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Currency)
                      .WithMany()
                      .HasForeignKey(a => a.CurrencyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transactions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.Date)
                      .IsRequired();
                entity.Property(e => e.Amount)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
                entity.Property(e => e.Comment)
                      .HasColumnType("nvarchar(255)")
                      .HasMaxLength(255)
                      .IsRequired(false);
                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()")
                      .IsRequired();

                entity.HasOne(t => t.Account)
                      .WithMany(a => a.Transactions)
                      .HasForeignKey(t => t.AccountId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Category)
                      .WithMany()
                      .HasForeignKey(t => t.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TypeTranslation>(entity =>
            {
                entity.ToTable("TypeTranslations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.LanguageCode)
                      .HasColumnType("nvarchar(5)")
                      .HasMaxLength(5)
                      .IsRequired();
                entity.Property(e => e.Name)
                      .HasColumnType("nvarchar(50)")
                      .HasMaxLength(50)
                      .IsRequired();
                entity.HasOne(e => e.TypeEntity)
                      .WithMany(t => t.Translations)
                      .HasForeignKey(e => e.TypeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CurrencyTranslation>(entity =>
            {
                entity.ToTable("CurrencyTranslations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.LanguageCode)
                      .HasColumnType("nvarchar(5)")
                      .HasMaxLength(5)
                      .IsRequired();
                entity.Property(e => e.CurrencyName)
                      .HasColumnType("nvarchar(50)")
                      .HasMaxLength(50)
                      .IsRequired();
                entity.HasOne(e => e.Currency)
                      .WithMany(c => c.Translations)
                      .HasForeignKey(e => e.CurrencyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CategoryTranslation>(entity =>
            {
                entity.ToTable("CategoryTranslations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.LanguageCode)
                      .HasColumnType("nvarchar(5)")
                      .HasMaxLength(5)
                      .IsRequired();
                entity.Property(e => e.Name)
                      .HasColumnType("nvarchar(50)")
                      .HasMaxLength(50)
                      .IsRequired();
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Translations)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
