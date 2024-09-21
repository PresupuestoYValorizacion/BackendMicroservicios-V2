
using AutoMapper;
using MsAcceso.Domain.Root.UsuarioLicencias;

namespace MsAcceso.Application.Mappers
{
    public class UsuarioLicenciaProfile : Profile
    {
        public UsuarioLicenciaProfile()
        {
            CreateMap<UsuarioLicencia, UsuarioLicenciaDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId!.Value!))
            .ForMember(dest => dest.LicenciaId, opt => opt.MapFrom(src => src.LicenciaId!.Value!))
            .ForMember(dest => dest.PeriodoLicenciaId, opt => opt.MapFrom(src => src.PeriodoLicenciaId!.Value!))
            .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => src.FechaInicio!))
            .ForMember(dest => dest.FechaFin, opt => opt.MapFrom(src => src.FechaFin!))
            ;

           
        }
    }
}