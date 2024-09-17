using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.MenuOpcions.GetMenuOpcionesBySistema;

internal sealed class GetMenuOpcionesBySistemaQueryHandler : IQueryHandler<GetMenuOpcionesBySistemaQuery, List<MenuOpcionDto>>
{
    private readonly IMenuOpcionRepository _menuOpcionRepository;
    private readonly IMapper _mapper;

    public GetMenuOpcionesBySistemaQueryHandler(
        IMenuOpcionRepository menuOpcionRepository,
        IMapper mapper
    )
    {
        _menuOpcionRepository = menuOpcionRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<MenuOpcionDto>>> Handle(GetMenuOpcionesBySistemaQuery request, CancellationToken cancellationToken)
    {
        var menuOpciones = await _menuOpcionRepository.GetAllMenuOpcionsByMenuId(request.SistemaId,cancellationToken);

        var menuOpcionDtos = _mapper.Map<List<MenuOpcionDto>>(menuOpciones);

        return menuOpcionDtos!;
    }


}