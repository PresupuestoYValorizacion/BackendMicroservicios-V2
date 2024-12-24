using AutoMapper;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Mappers;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<ClienteTenant,ClienteDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
        .ForMember(dest => dest.NumeroDocumento, opt => opt.MapFrom(src => src.NumeroDocumento))
        .ForMember(dest => dest.TipoClienteId, opt => opt.MapFrom(src => src.TipoClienteId))
        .ForMember(dest => dest.TipoCliente, opt => opt.MapFrom(src => src.TipoCliente!.Nombre))
        .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId))
        .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento!.Nombre))
        .ForMember(dest => dest.TipoPersonaId, opt => opt.MapFrom(src => src.TipoPersonaId))
        .ForMember(dest => dest.TipoPersona, opt => opt.MapFrom(src => src.TipoPersona!.Nombre))

        ;
    }
}