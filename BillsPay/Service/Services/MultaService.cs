using AutoMapper;
using Data.Repositories;
using Domain.DTO.Requests;
using Domain.DTO.Responses;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MultaService : GenericService
    {
        public MultaService(Repository repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public async Task<MultaReponse> GetByDaysLatePayment(GetMultaByDaysLatePaymentRequest request)
        {
            var entity = Repository.GetAll<Multa>()
                .Where(e => e.PeriodoMaximoDeDiasEmAtraso >= request.Days)
                .OrderBy(e => e.PeriodoMaximoDeDiasEmAtraso)
                .FirstOrDefault();

            if (entity == null)
            {
                entity = Repository.GetAll<Multa>()
                    .Where(e => e.PeriodoMaximoDeDiasEmAtraso == null)
                    .FirstOrDefault();
            }

            return Mapper.Map<MultaReponse>(entity);
        }
    }
}
