using AutoMapper;
using Domain.DTO.Requests;
using Domain.Entities;

namespace CrossCutting.AutoMapper
{
    public class AutoMapperRequestProfile : Profile
    {
        public AutoMapperRequestProfile()
        {
            CreateMap<CreateContaAPagarRequest, ContaAPagar>();
        }
    }
}


