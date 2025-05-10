using AutoMapper;
using SchoolAPI.DTOs;
using SchoolAPI.Models;

namespace SchoolAPI
{
    public class SchoolsAutoMapperProfile : Profile
    {
        public SchoolsAutoMapperProfile()
        {

            CreateMap<School, SchoolDto>();

         
            CreateMap<SchoolDto, School>()
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => "")); 
        }
    }
}