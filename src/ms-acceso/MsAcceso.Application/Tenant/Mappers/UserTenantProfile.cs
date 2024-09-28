
using AutoMapper;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Mappers
{
    public class UserTenantProfile : Profile
    {
        public UserTenantProfile()
        {
            CreateMap<UserTenant, UserTenantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email!))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username!))
            .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Persona!))
            .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.PersonaId!.Value!))
            .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.RolId!.Value!))
            // .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.Rol!.!.Value == TipoRol.Administrador))
            // .ForMember(dest => dest.Licencia, opt => opt.MapFrom(src => src.UsuarioLicencias != null && src.UsuarioLicencias.Count > 0 ? src.UsuarioLicencias.First() : null))
            ;


        }
    }
}