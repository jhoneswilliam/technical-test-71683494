using AutoMapper;
using Domain.DTO.Responses;
using Domain.Entities;

namespace CrossCutting.AutoMapper
{
    public class AutoMapperResponsesProfile : Profile
    {
        public AutoMapperResponsesProfile()
        {
            CreateMap<ContaAPagar, ContaAPagarReponse>();
            CreateMap<Multa, MultaReponse>();            
        }
    }
}


