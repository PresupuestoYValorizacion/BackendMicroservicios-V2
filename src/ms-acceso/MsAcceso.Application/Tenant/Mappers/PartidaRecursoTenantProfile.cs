using AutoMapper;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;

namespace MsAcceso.Application.Tenant.Mappers
{
    public class PartidaRecursoTenantProfile : Profile
    {
        public PartidaRecursoTenantProfile()
        {
            CreateMap<PartidaRecursoTenant, PartidaRecursoTenantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.PartidaId, opt => opt.MapFrom(src => src.PartidaId!.Value.ToString()))
            .ForMember(dest => dest.RecursoId, opt => opt.MapFrom(src => src.RecursoId!.Value.ToString()))
            .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
            .ForMember(dest => dest.Cuadrilla, opt => opt.MapFrom(src => src.Cuadrilla))
            .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Precio))
            .ForMember(dest => dest.Parcial, opt => opt.MapFrom(src => src.Parcial))
            ;
        }
    }
}
    // public PartidaTenantId? PartidaId  {get; private set;}
    // public RecursoTenantId? RecursoId  {get; private set;}
    // public PartidaTenant? Partida {get; private set;}
    // public RecursoTenant? Recurso  {get; private set;}
    // public int? Cantidad {get; private set;}
    // public int? Cuadrilla  {get; private set;}
    // public double? Precio {get; private set;}
    // public double? Parcial {get; private set;}