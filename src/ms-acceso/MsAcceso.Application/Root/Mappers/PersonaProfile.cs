
using AutoMapper;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Domain.Root.PersonasNaturales;

namespace MsAcceso.Application.Root.Mappers
{
    public class PersonaProfile : Profile
    {
        public PersonaProfile()
        {
            CreateMap<Persona, PersonaDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo!.Nombre))
            .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento!.Nombre))
            .ForMember(dest => dest.NumeroDocumento, opt => opt.MapFrom(src => src.NumeroDocumento!))
            .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId!.Value!))
            .ForMember(dest => dest.TipoId, opt => opt.MapFrom(src => src.TipoId!.Value!))
            ;

            CreateMap<PersonaNatural, PersonaNaturalDto>()
             .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.NombreCompleto ));

            CreateMap<PersonaJuridica, PersonaJuridicaDto>()
            .ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.RazonSocial!));
        }
    }
}