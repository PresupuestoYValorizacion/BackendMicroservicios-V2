using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Opciones.GetAllOpcionQuery;


internal sealed class GetAllOpcionQueryHandler : IQueryHandler<GetAllOpcionQuery, List<OpcionDto>>
{
    private readonly IOpcionRepository _opcionRepository;
    private readonly IMapper _mapper;

    public GetAllOpcionQueryHandler(
        IOpcionRepository opcionRepository,
        IMapper mapper
        )
    {
        _opcionRepository = opcionRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<OpcionDto>>> Handle(GetAllOpcionQuery request, CancellationToken cancellationToken)
    {

        var opciones = await _opcionRepository.GetAllOpcion(cancellationToken);

        var opcionesDto = _mapper.Map<List<OpcionDto>>(opciones);

        return opcionesDto!;
    }
}