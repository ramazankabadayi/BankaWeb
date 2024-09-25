using BankaWebEL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankaWebDL
{
    public class BankaWebDbContext : IdentityDbContext<IdentityUser>
    {
        public BankaWebDbContext(DbContextOptions<BankaWebDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);          
            modelBuilder.Entity<ExchangeRate>()
                        .HasOne(e => e.FromCurrency)
                        .WithMany()
                        .HasForeignKey(e => e.FromCurrencyId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExchangeRate>()
                        .HasOne(e => e.ToCurrency)
                        .WithMany()
                        .HasForeignKey(e => e.ToCurrencyId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                        .HasOne(t => t.Currency)
                        .WithMany() 
                        .HasForeignKey(t => t.CurrencyId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfer>()
                        .HasOne(t => t.SenderAccount)
                        .WithMany()
                        .HasForeignKey(t => t.SenderAccountId)
                        .OnDelete(DeleteBehavior.Restrict); 

            
            modelBuilder.Entity<Transfer>()
                        .HasOne(t => t.ReceiverAccount)
                        .WithMany()
                        .HasForeignKey(t => t.ReceiverAccountId)
                        .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
