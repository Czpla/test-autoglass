namespace App.Infra.Data.Mappers
{
    using Infra.Data.Mappers.Base;
    using Core.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class ProductMapper : EntityMapper<Product>, IEntityTypeConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.ToTable("products");

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(x => x.Situation)
                .HasColumnName("situation")
                .HasColumnType("varchar(10)")
                .IsRequired();

            builder.Property(x => x.ManufacturingDate)
                .HasColumnName("manufacturing_date")
                .HasColumnType("date")
                .IsRequired(false);

            builder.Property(x => x.ExpirationDate)
                .HasColumnName("expiration_date")
                .HasColumnType("date")
                .IsRequired(false);

            builder.Property(x => x.SupplierCode)
                .HasColumnName("supplier_code")
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(x => x.SupplierDescription)
                .HasColumnName("supplier_description")
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            builder.Property(x => x.SupplierCnpj)
                .HasColumnName("supplier_cnpj")
                .HasColumnType("varchar(14)")
                .IsRequired(false);
        }
    }
}
