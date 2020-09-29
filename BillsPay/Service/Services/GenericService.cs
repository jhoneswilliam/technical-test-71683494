using AutoMapper;
using Data.Repositories;

namespace Service.Services
{
    public class GenericService
    {
        protected Repository Repository { get; set; }
        protected IMapper Mapper { get; set; }

        public GenericService(Repository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper; 
        }
    }
}
