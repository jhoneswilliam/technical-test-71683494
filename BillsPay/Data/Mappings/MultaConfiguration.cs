using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.Mappings
{
    public class MultaConfiguration : IEntityTypeConfiguration<Multa>
    {
        public void Configure(EntityTypeBuilder<Multa> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(e => e.PeriodoMaximoDeDiasEmAtraso);
            builder.Property(e => e.PorcentagemAplicadaMulta).IsRequired();

            builder.HasData(new Multa[] {
                new Multa {
                    Id = Guid.Parse("557256ce-83b3-4892-9acd-c8b03ed6a9de"),
                    PeriodoMaximoDeDiasEmAtraso = 3,
                    PorcentagemAplicadaDiaAtraso = 0.1 / 100.0,
                    PorcentagemAplicadaMulta  = 2.0 / 100.0
                },

                new Multa {
                    Id = Guid.Parse("6c9f40d0-89c4-4326-8b49-fa48cbaa521a"),
                    PeriodoMaximoDeDiasEmAtraso = 5,
                    PorcentagemAplicadaDiaAtraso = 0.2 / 100.0,
                    PorcentagemAplicadaMulta  = 3.0 / 100.0
                },

                new Multa {
                    Id = Guid.Parse("538aafe5-4edf-4e0a-a629-67e36a1a11cb"),
                    PeriodoMaximoDeDiasEmAtraso = null,
                    PorcentagemAplicadaDiaAtraso = 0.3 / 100.0,
                    PorcentagemAplicadaMulta  = 5.0 / 100.0
                },
            });
        }
    }
}
