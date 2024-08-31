
using AutoMapper;
using MsAcceso.Domain.Root.Resenias;

namespace MsAcceso.Application.Mappers
{
    public class ReseniaProfile : Profile
    {
        public ReseniaProfile()
        {
            CreateMap<Resenia, ReseniaDto>()
            .ForMember(dest => dest.Comentario, opt => opt.MapFrom(src => src.Comentario!))
            .ForMember(dest => dest.Calificacion, opt => opt.MapFrom(src => src.Calificacion!));
        }
    }
}