using AutoMapper;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Mappers;

public class SistemaProfile : Profile
{
    public SistemaProfile()
    {
        CreateMap<Sistema,SistemaDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Dependencia, opt => opt.MapFrom(src => src.Dependencia!.Value.ToString()))
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
        .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo))
        .ForMember(dest => dest.Nivel, opt => opt.MapFrom(src => src.Nivel))
        .ForMember(dest => dest.Childrens, opt => opt.MapFrom(src => src.Sistemas))
        .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));
    }
}