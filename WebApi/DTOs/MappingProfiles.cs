using AutoMapper;
using Core.Entities;
using WebApi.DTOs.PublicationDtos;
using WebApi.DTOs.UserDtos;

namespace WebApi.DTOs
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // user
            CreateMap<UserEntities, ResponseUserDto>();
            CreateMap<RegistrationDto, UserEntities>();
            
            // publications
            CreateMap<PublicationDto, PublicationsEntities>();
            CreateMap<PublicationsEntities, ResponsePublicationDto>();
        }
    }
}
