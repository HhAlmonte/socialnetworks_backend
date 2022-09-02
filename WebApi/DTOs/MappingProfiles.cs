using AutoMapper;
using Core.Entities;

namespace WebApi.DTOs
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserEntities, RegistrationDto>();
                /*.ForMember(u => u.Name, r => r.MapFrom(n => n.Name));*/
        }
    }
}
