using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.RolPermisosOpciones;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Roles.AddPermisos;

internal sealed class AddPermisosCommandHandler : ICommandHandler<AddPermisosCommand, Guid>
{

    private readonly IMapper _mapper;
    private readonly ISistemaRepository _sistemaRepository;

    private readonly IRolPermisoRepository _rolPermisoRepository;
    private readonly IRolPermisoOpcionRepository _rolPermisoOpcionRepository;

    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public AddPermisosCommandHandler(
        IMapper mapper,
        ISistemaRepository sistemaRepository,
        IRolPermisoOpcionRepository rolPermisoOpcionRepository,
        IRolPermisoRepository rolPermisoRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _mapper = mapper;
        _sistemaRepository = sistemaRepository;
        _rolPermisoOpcionRepository = rolPermisoOpcionRepository;
        _rolPermisoRepository = rolPermisoRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(AddPermisosCommand request, CancellationToken cancellationToken)
    {
        var sistemasRequest = request.SistemasRequest;

        if (sistemasRequest.Count > 0)
        {
            foreach (var sistema in sistemasRequest)
            {
                await ProcesarSistema(sistema, request.RolId, cancellationToken);
            }
        }

        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);
        return Result.Success(request.RolId.Value, Message.Update);
    }

    // Método recursivo para procesar el sistema y sus childrens
    private async Task ProcesarSistema(SistemaRequest sistema, RolId rolId, CancellationToken cancellationToken)
    {
        var sistemaEncontrado = await _sistemaRepository.GetSistemaByIdAndRol(rolId, new SistemaId(sistema.Id), cancellationToken);
        var sistemaDto = _mapper.Map<SistemaByRolDto>(sistemaEncontrado);

        RolPermiso? rolPermiso = await _rolPermisoRepository.GetByMenuAndRol(new SistemaId(sistema.Id), rolId);

        // Comparar el estado 'Completed' del sistema
        if (sistemaDto.Completed != sistema.Completed)
        {
            if (rolPermiso == null && sistema.Completed)
            {
                rolPermiso = RolPermiso.Create(rolId, new SistemaId(sistema.Id));
                _rolPermisoRepository.Add(rolPermiso);
            }
            else if (!sistema.Completed && rolPermiso != null)
            {
                _rolPermisoRepository.Delete(rolPermiso);
            }
        }

        // Procesar MenuOpciones
        if (sistema.Completed && rolPermiso != null)
        {
            if (sistema.MenuOpciones.Count > 0)
            {
                foreach (var menuOpcion in sistema.MenuOpciones)
                {
                    var menuOpcionDto = sistemaDto.MenuOpciones!
                                        .FirstOrDefault(mo => mo.OpcionId == menuOpcion.OpcionId.ToString());

                    if (menuOpcionDto != null)
                    {
                        if (menuOpcion.Completed != menuOpcionDto.Completed)
                        {
                            var rolPermisoOpcion = await _rolPermisoOpcionRepository.GetByOpcionAndRolPermiso(
                                rolPermiso.Id!, new OpcionId(menuOpcion.OpcionId));

                            if (rolPermisoOpcion == null && menuOpcion.Completed)
                            {
                                var newRolPermisoOpcion = RolPermisoOpcion.Create(
                                    rolPermiso.Id!, new OpcionId(menuOpcion.OpcionId));
                                _rolPermisoOpcionRepository.Add(newRolPermisoOpcion);
                            }
                            else if (!menuOpcion.Completed && rolPermisoOpcion != null)
                            {
                                _rolPermisoOpcionRepository.Delete(rolPermisoOpcion);
                            }
                        }
                    }
                }
            }
        }

        // Llamada recursiva para los childrens
        if (sistema.Childrens.Count > 0)
        {
            foreach (var child in sistema.Childrens)
            {
                await ProcesarSistema(child, rolId, cancellationToken);
            }
        }
    }
}