using AutoMapper;
using Data.Repositories;
using Domain.DTO.Requests;
using Domain.DTO.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MultaService : GenericService
    {
        public MultaService(Repository repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public async Task<MultaReponse> GetByDaysLatePayment(GetMultaByDaysLatePayment request)
        {
            var entity = await Repository.GetAll<Multa>()
                .AsNoTracking()
                .Where(e => e.PeriodoMaximoDeDiasEmAtraso >= request.Days)
                .OrderBy(e => e.PeriodoMaximoDeDiasEmAtraso)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                entity = await Repository.GetAll<Multa>()
                    .Where(e => e.PeriodoMaximoDeDiasEmAtraso == null)
                    .FirstOrDefaultAsync();
            }

            return Mapper.Map<MultaReponse>(entity);
        }
    }
}
