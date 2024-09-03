using AutoMapper;
using MsAcceso.Domain.Root.Categorias;

namespace MsAcceso.Application.Mappers;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<Categoria,CategoriaDto>()
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));
    }
}