using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();

            builder.Property(si => si.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(x => x.ProductName)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(si => si.Product)
                .WithMany(p => p.SaleItems)
                .HasForeignKey(si => si.ProductId)
                .HasConstraintName("FK_saleitems_products")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(si => si.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            builder.Property(si => si.UnitPrice)
                .HasColumnName("unit_price")
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.Property(si => si.TotalPrice)
                .HasColumnName("total_price")
                .HasColumnType("numeric(18,2)");

            builder.Property(si => si.Discount)
                .HasColumnName("discount")
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.Property(s => s.CancellationDate)
               .HasColumnName("cancellation_date")
               .HasColumnType("timestamp with time zone");
           
            builder.Property(s => s.IsCancelled)
               .HasColumnName("is_cancelled")
               .HasDefaultValue(false);
        }
    }
}
