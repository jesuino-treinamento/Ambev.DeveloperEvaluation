using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales"); 

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.SaleNumber)
                .HasColumnName("sale_number")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.SaleDate)
                .HasColumnName("sale_date")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(s => s.TotalAmount)
                .HasColumnName("total_amount")
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasIndex(s => s.SaleNumber)
                .HasDatabaseName("ix_sales_number")
                .IsUnique();

            builder.HasIndex(s => s.SaleDate)
                .HasDatabaseName("ix_sales_date");

            builder.Property(s => s.CancellationDate)
                .HasColumnName("cancellation_date")
                .HasColumnType("timestamp with time zone");

            builder.Property(s => s.IsCancelled)
               .HasColumnName("is_cancelled")
               .HasDefaultValue(false);

            builder.HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId)
                .HasConstraintName("FK_sales_customers")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Branch)
                .WithMany(b => b.Sales)
                .HasForeignKey(s => s.BranchId)
                .HasConstraintName("FK_sales_branches")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Items)
                .WithOne(si => si.Sale)
                .HasForeignKey(si => si.SaleId)
                .HasConstraintName("FK_salesitems")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
