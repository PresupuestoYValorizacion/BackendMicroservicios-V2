
using AutoMapper;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Domain.Root.PersonasNaturales;

namespace MsAcceso.Application.Mappers
{
    public class PersonaProfile : Profile
    {
        public PersonaProfile()
        {
            CreateMap<Persona, PersonaDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo!.Nombre))
            .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento!.Nombre))
            .ForMember(dest => dest.NumeroDocumento, opt => opt.MapFrom(src => src.NumeroDocumento!));

            CreateMap<PersonaNatural, PersonaNaturalDto>()
            .ForMember(dest => dest.ApellidoMaterno, opt => opt.MapFrom(src => src.ApellidoMaterno!))
            .ForMember(dest => dest.ApellidoPaterno, opt => opt.MapFrom(src => src.ApellidoPaterno!))
            .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres!));

            CreateMap<PersonaJuridica, PersonaJuridicaDto>()
            .ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.RazonSocial!));
        }
    }
}