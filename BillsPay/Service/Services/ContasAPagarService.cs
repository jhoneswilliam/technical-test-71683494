using AutoMapper;
using Data.Repositories;
using Domain.DTO.Requests;
using Domain.DTO.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ContasAPagarService : GenericService
    {
        private MultaService MultaService { get; set; }

        public ContasAPagarService(Repository repository, IMapper mapper, MultaService multaService) : base(repository, mapper)
        {
            MultaService = multaService;
        }

        public async Task<ContaAPagarReponse> GetAll()
        {
            var entities = await Repository.GetAll<ContaAPagar>()
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return Mapper.Map<ContaAPagarReponse>(entities);
        }

        public async Task<ContaAPagarReponse> Create(CreateContaAPagarRequest request)
        {
            var entity = Mapper.Map<ContaAPagar>(request);

            var diffDays = entity.DataPagamento.Date.Subtract(entity.DataVencimento.Date).TotalDays;
            entity.QuantidadeDiasEmAtraso = Math.Max(Convert.ToInt32(Math.Ceiling(diffDays)), 0);

            if (entity.QuantidadeDiasEmAtraso > 0)
            {
                var punishmentRule = await MultaService.GetByDaysLatePayment(new GetMultaByDaysLatePayment
                {
                    Days = entity.QuantidadeDiasEmAtraso
                });

                entity.MultaAplicadaId = punishmentRule.Id;
                var originalValueDouble = Convert.ToDouble(entity.ValorOriginal);
                var punishment = punishmentRule.PorcentagemAplicadaMulta * originalValueDouble;
                var punishmentDays = punishmentRule.PorcentagemAplicadaDiaAtraso
                    * entity.QuantidadeDiasEmAtraso
                    * originalValueDouble;

                entity.ValorCorrigido = Convert.ToDecimal(originalValueDouble + punishment + punishmentDays);
            }

            Repository.Add(entity);

            return Mapper.Map<ContaAPagarReponse>(entity);
        }
    }
}
