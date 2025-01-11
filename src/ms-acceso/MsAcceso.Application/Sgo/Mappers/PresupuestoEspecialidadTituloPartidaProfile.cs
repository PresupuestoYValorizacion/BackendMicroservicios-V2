using AutoMapper;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;

namespace MsAcceso.Application.Sgo.Mappers;

public class PresupuestoEspecialidadTituloPartidaProfile : Profile
{
    public PresupuestoEspecialidadTituloPartidaProfile()
    {
        CreateMap<PresupuestoEspecialidadTituloPartidaTenant,PresupuestoEspecialidadTituloPartidaTenantDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Partida, opt => opt.MapFrom(src => src.Partida))
        .ForMember(dest => dest.Correlativo, opt => opt.MapFrom(src => src.Correlativo))
        // .ForMember(dest => dest., opt => opt.MapFrom(src => src.PartidaId))
        ;
    }
}