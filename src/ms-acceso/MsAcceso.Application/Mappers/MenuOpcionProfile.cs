using AutoMapper;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.Mappers;

public class MenuOpcionProfile : Profile
{
    public MenuOpcionProfile()
    {
        CreateMap<MenuOpcion,MenuOpcionDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.OpcionId, opt => opt.MapFrom(src => src.OpcionesId!.Value.ToString()))
        .ForMember(dest => dest.SistemaId, opt => opt.MapFrom(src => src.MenusId!.Value.ToString()))
        .ForMember(dest => dest.Completed, opt => opt.MapFrom(src =>
        src.Opcion != null && src.Opcion.RolPermisoOpcions != null &&
        src.Opcion.RolPermisoOpcions.Any(rpo =>
            rpo.RolPermiso != null &&
            rpo.RolPermiso.MenuId != null &&
            rpo.RolPermiso.MenuId == src.MenusId
        )
    ));

    }
}