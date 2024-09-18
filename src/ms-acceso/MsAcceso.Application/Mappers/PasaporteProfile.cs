
using AutoMapper;
using MsAcceso.Domain.Root.Pasaportes;

namespace MsAcceso.Application.Mappers
{
    public class PasaporteProfile : Profile
    {
        public PasaporteProfile()
        {
            CreateMap<Pasaporte, PasaporteDto>()
            .ForMember(dest => dest.NroSerie, opt => opt.MapFrom(src => src.NroSerie!));
        }
    }
}