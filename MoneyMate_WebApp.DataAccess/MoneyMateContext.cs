using Microsoft.EntityFrameworkCore;
using MoneyMate_WebApp.DataAccess.Entities;

namespace MoneyMate_WebApp.DataAccess
{
    public class MoneyMateContext : DbContext
    {
        public MoneyMateContext()
        {
        }

        public MoneyMateContext(DbContextOptions<MoneyMateContext> options)
        : base(options)
        {
        }


        public DbSet<TypeEntity> Types { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ADMIN-PC\\SQLEXPRESS;Database=MoneyMateDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeEntity>(entity =>
            {
                entity.ToTable("Types");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.Name)
                      .HasMaxLength(50)
                      .IsRequired();
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currencies");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CurrencyCode)
                      .HasMaxLength(10)
                      .IsRequired();
                entity.Property(e => e.CurrencyName)
                      .HasMaxLength(50)
                      .IsRequired();
                entity.Property(e => e.Symbol)
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
                entity.Property(e => e.Name)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.Type)
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
                      .HasMaxLength(255);
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
        }
    }
}
