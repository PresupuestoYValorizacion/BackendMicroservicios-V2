
using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Opciones.GetByIdOpcion;

internal sealed class GetByIdOpcionQueryHandler : IQueryHandler<GetByIdOpcionQuery, OpcionDto>
{
    private readonly IOpcionRepository _opcionRepository;
    private readonly IMapper _mapper;

    public GetByIdOpcionQueryHandler(
        IOpcionRepository opcionRepository,
        IMapper mapper
    )
    {
        _opcionRepository = opcionRepository;
        _mapper = mapper;
    }

    public async Task<Result<OpcionDto>> Handle(GetByIdOpcionQuery request, CancellationToken cancellationToken)
    {
        var opcion = await _opcionRepository.GetByIdAsync(new OpcionId(request.Id), cancellationToken);

        if(opcion is null)
        {
            return Result.Failure<OpcionDto>(OpcionErrors.NotFound)!;
        }

        var opcionDTO = _mapper.Map<OpcionDto>(opcion);

        return opcionDTO!;
    }
}