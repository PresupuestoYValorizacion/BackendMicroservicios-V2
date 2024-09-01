using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.MenuOpcions.GetMenuOpcionById;

internal sealed class GetMenuOpcionByIdQueryHandler : IQueryHandler<GetMenuOpcionByIdQuery, MenuOpcionDto?>
{
    private readonly IMenuOpcionRepository _menuOpcionRepository;
    private readonly IMapper _mapper;

    public GetMenuOpcionByIdQueryHandler(
        IMenuOpcionRepository menuOpcionRepository,
        IMapper mapper
    )
    {
        _menuOpcionRepository = menuOpcionRepository;
        _mapper = mapper;
    }

    public async Task<Result<MenuOpcionDto?>> Handle(GetMenuOpcionByIdQuery request, CancellationToken cancellationToken)
    {
        var menuOpcionId = new Guid(request.Id!);

        var menuOpcion = await _menuOpcionRepository.GetMenuOpcionById(new MenuOpcionId(menuOpcionId),cancellationToken); 

        if(menuOpcion is null)
        {
            return Result.Failure<MenuOpcionDto>(MenuOpcionErrors.MenuOpcionNotFound);
        }

        var menuOpcionDto = _mapper.Map<MenuOpcionDto>(menuOpcion);

        return menuOpcionDto;
    }
}