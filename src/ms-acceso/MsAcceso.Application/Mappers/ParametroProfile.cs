using AutoMapper;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Mappers;

public class ParametroProfile : Profile
{
    public ParametroProfile()
    {
        CreateMap<Parametro,ParametroDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
        .ForMember(dest => dest.Abreviatura, opt => opt.MapFrom(src => src.Abreviatura))
        .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
        .ForMember(dest => dest.Dependencia, opt => opt.MapFrom(src => src.Dependencia!.Value))
        .ForMember(dest => dest.Nivel, opt => opt.MapFrom(src => src.Nivel!.Value))
        .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor));
    }
}