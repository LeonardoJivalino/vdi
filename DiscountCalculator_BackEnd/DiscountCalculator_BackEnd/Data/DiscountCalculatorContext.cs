using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DiscountCalculator_BackEnd.Data
{
    public partial class DiscountCalculatorContext : DbContext
    {
        public DiscountCalculatorContext()
        {
        }

        public DiscountCalculatorContext(DbContextOptions<DiscountCalculatorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerDiscount> CustomerDiscounts { get; set; } = null!;
        public virtual DbSet<Transaksi> Transaksis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=DiscountCalculator;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDiscount>(entity =>
            {
                entity.HasKey(e => e.TipeCustomer)
                    .HasName("PK__Customer__F05500CF67B5B7EF");

                entity.ToTable("CustomerDiscount");

                entity.Property(e => e.TipeCustomer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountFormula)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaksi>(entity =>
            {
                entity.ToTable("Transaksi");

                entity.Property(e => e.TransaksiId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TransaksiID");

                entity.Property(e => e.TipeCustomer)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
