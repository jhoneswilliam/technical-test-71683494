using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class ContaAPagarConfiguration : IEntityTypeConfiguration<ContaAPagar>
    {
        public void Configure(EntityTypeBuilder<ContaAPagar> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(e => e.Nome).HasMaxLength(255).IsRequired();
            builder.Property(e => e.ValorOriginal).IsRequired();
            builder.Property(e => e.ValorCorrigido).IsRequired();
            builder.Property(e => e.QuantidadeDiasEmAtraso).IsRequired();
            builder.Property(e => e.DataPagamento).IsRequired();

            builder.Property(e => e.MultaAplicadaId);
            builder.HasOne(d => d.MultaAplicada)
                   .WithMany(p => p.ContaAPagarMultadas)
                   .HasForeignKey(d => d.MultaAplicadaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
