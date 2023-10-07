using AutoMapper;
using Microsoft.Identity.Client;
using World.Api.DTO.Country;
using World.Api.DTO.States;
using World.Api.Migrations;
using World.Api.Models;

namespace World.Api.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country,CreateCountryDTO>().ReverseMap();
            CreateMap<Country,CountryDTO>().ReverseMap();
            CreateMap<Country,UpdateCountryDto>().ReverseMap();

            CreateMap<States,CreateStatesDto>().ReverseMap();
            CreateMap<States,UpdateStatesDto>().ReverseMap();
            CreateMap<States,StatesDto>().ReverseMap();
        }           
    }
}
