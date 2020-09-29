using AutoMapper;
using Data.Repositories;

namespace Service.Services
{
    public class ContasAPagarService : GenericService
    {
        public ContasAPagarService(Repository repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}
