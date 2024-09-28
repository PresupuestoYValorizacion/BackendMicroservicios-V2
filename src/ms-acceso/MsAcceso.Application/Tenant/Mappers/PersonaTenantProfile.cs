
using AutoMapper;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;

namespace MsAcceso.Application.Tenant.Mappers
{
    public class PersonaTenantProfile : Profile
    {
        public PersonaTenantProfile()
        {
            CreateMap<PersonaTenant, PersonaTenantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo!.Nombre))
            .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento!.Nombre))
            .ForMember(dest => dest.NumeroDocumento, opt => opt.MapFrom(src => src.NumeroDocumento!))
            .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId!))
            .ForMember(dest => dest.TipoId, opt => opt.MapFrom(src => src.TipoId!))
            ;

            CreateMap<PersonaNaturalTenant, PersonaNaturalTenantDto>()
             .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.NombreCompleto ));

            CreateMap<PersonaJuridicaTenant, PersonaJuridicaTenantDto>()
            .ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.RazonSocial!));
        }
    }
}