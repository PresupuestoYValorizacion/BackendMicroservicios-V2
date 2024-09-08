using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Parametros.ValidarAccesoMenu;

internal sealed class ValidarAccesoMenuQueryHandler : IQueryHandler<ValidarAccesoMenuQuery, bool>
{
    private readonly IRolPermisoRepository _rolPermisoRepository;
    private readonly ISistemaRepository _sistemaRepository;

    private readonly IMapper _mapper;

    public ValidarAccesoMenuQueryHandler(
        ISistemaRepository sistemaRepository,
        IRolPermisoRepository rolPermisoRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _rolPermisoRepository = rolPermisoRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(ValidarAccesoMenuQuery request, CancellationToken cancellationToken)
    {

        var sistema = await _sistemaRepository.GetByUrlAsync(request.Url!, cancellationToken);


        if(sistema is null)
        {
            return Result.Failure<bool>(SistemaErrors.SistemaNotFound);
        }

        var existePermiso = await _rolPermisoRepository.GetByMenuAndRol(sistema.Id!, request.RolId!, cancellationToken);


        return existePermiso is not null;

    }


}