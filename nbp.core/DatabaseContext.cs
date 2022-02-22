using System.Dynamic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using nbp.core.dto;

namespace nbp.core
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<CurrencyInfoDto> Currencies { get; set; }
        public virtual DbSet<ExchangeRateDto> ExchangeRate { get; set; }
        public virtual DbSet<ExchangeRateTableDto> ExchangeTables { get; set; }
        
        public DatabaseContext (DbContextOptions<DatabaseContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyInfoDto>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired(true)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .IsRequired(true)
                    .HasMaxLength(100)
                    .IsUnicode(true);

                entity.HasIndex(e => e.Code);
            });

            modelBuilder.Entity<ExchangeRateDto>(entity =>
            {
                entity.HasIndex(e => e.CurrencyId);
                entity.HasIndex(e => e.TableId);
            });

            modelBuilder.Entity<ExchangeRateTableDto>(entity =>
            {
                entity.Property(e => e.No)
                    .IsRequired(true)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Table)
                    .IsRequired(true)
                    .HasMaxLength(1)
                    .IsUnicode(false);
                entity.HasIndex(e => e.EffectiveDate);
                entity.HasIndex(e => e.No);
            });
        }
    }
}
