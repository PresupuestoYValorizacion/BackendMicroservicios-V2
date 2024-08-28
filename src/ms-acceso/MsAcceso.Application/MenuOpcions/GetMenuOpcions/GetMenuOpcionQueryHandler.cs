using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.MenuOpcions.GetMenuOpcions;

internal sealed class GetMenuOpcionQueryHandler : IQueryHandler<GetMenuOpcionQuery, List<MenuOpcionDto>?>
{
    private readonly IMenuOpcionRepository _menuOpcionRepository;
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IMapper _mapper;

    public GetMenuOpcionQueryHandler(
        IMenuOpcionRepository menuOpcionRepository,
        ISistemaRepository sistemaRepository,
        IMapper mapper
    )
    {
        _menuOpcionRepository = menuOpcionRepository;
        _sistemaRepository = sistemaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<MenuOpcionDto>?>> Handle(GetMenuOpcionQuery request, CancellationToken cancellationToken)
    {
        var menuId = new SistemaId(Guid.Parse(request.SistemaId!));

        var menuExists = await _sistemaRepository.SistemaGetByIdAsync(menuId,cancellationToken);

        if(menuExists is null)
        {
            return Result.Failure<List<MenuOpcionDto>>(SistemaErrors.SistemaNotFound);
        }

        var menusOpciones = await _menuOpcionRepository.GetAllMenuOpcionsByMenuId(menuId,cancellationToken);

        var menusOpcionesDto = _mapper.Map<List<MenuOpcionDto>>(menusOpciones);

        return menusOpcionesDto;
    }
}