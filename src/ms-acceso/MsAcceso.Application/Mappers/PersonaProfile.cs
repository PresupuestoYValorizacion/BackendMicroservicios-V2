
using AutoMapper;
using MsAcceso.Domain.Root.Personas;

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
            .ForMember(dest => dest.NumeroDocumento, opt => opt.MapFrom(src => src.NumeroDocumento!))
            .ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.RazonSocial!));

        }
    }
}