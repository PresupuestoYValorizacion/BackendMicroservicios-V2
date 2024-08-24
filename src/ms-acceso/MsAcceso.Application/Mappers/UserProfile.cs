
using AutoMapper;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email!))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username!))
            .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa!))
            .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.EmpresaId!.Value!))
            ;

    
        }
    }
}