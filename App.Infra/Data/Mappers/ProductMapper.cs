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
        }
    }
}
