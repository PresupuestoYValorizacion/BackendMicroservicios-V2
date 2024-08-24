using AutoMapper;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.Mappers;

public class MenuOpcionProfile : Profile
{
    public MenuOpcionProfile()
    {
        CreateMap<MenuOpcion,MenuOpcionDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.OpcionId, opt => opt.MapFrom(src => src.OpcionId!.Value.ToString()))
        .ForMember(dest => dest.SistemaId, opt => opt.MapFrom(src => src.MenuId!.Value.ToString()));

    }
}