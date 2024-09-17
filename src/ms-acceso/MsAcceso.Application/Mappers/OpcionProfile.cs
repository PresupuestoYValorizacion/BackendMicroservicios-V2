using AutoMapper;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Mappers;

public class OpcionProfile : Profile
{
    public OpcionProfile()
    {
        CreateMap<Opcion,OpcionDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
        .ForMember(dest => dest.Icono, opt => opt.MapFrom(src => src.Icono!))
        .ForMember(dest => dest.Tooltip, opt => opt.MapFrom(src => src.Tooltip!));
    }
}