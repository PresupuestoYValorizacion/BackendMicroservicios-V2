using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Root.Roles.AddPermisos;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;
using MsAcceso.Domain.Tenant.RolPermisosTenant;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.AddPermisosTenant;

internal sealed class AddPermisosTenantCommandHandler : ICommandHandler<AddPermisosTenantCommand, Guid>
{

    private readonly IMapper _mapper;
    private readonly ISistemaRepository _sistemaRepository;

    private readonly IRolPermisoTenantRepository _rolPermisoTenantRepository;
    private readonly IRolPermisoOpcionTenantRepository _rolPermisoOpcionTenantRepository;

    private readonly IUnitOfWorkApplication _unitOfWork;

    public AddPermisosTenantCommandHandler(
        IMapper mapper,
        ISistemaRepository sistemaRepository,
        IRolPermisoOpcionTenantRepository rolPermisoOpcionTenantRepository,
        IRolPermisoTenantRepository rolPermisoTenantRepository,
        IUnitOfWorkApplication unitOfWorkTenant
    )
    {
        _mapper = mapper;
        _sistemaRepository = sistemaRepository;
        _rolPermisoOpcionTenantRepository = rolPermisoOpcionTenantRepository;
        _rolPermisoTenantRepository = rolPermisoTenantRepository;
        _unitOfWork = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(AddPermisosTenantCommand request, CancellationToken cancellationToken)
    {
        var sistemasRequest = request.SistemasRequest;

        if (sistemasRequest.Count > 0)
        {
            foreach (var sistema in sistemasRequest)
            {
                await ProcesarSistema(sistema, request.RolId, cancellationToken);
            }
        }

        await _rolPermisoTenantRepository.SaveChangesAsync(cancellationToken);
        await _rolPermisoOpcionTenantRepository.SaveChangesAsync(cancellationToken);
        return Result.Success(request.RolId.Value, Message.Update);
    }

    private async Task ProcesarSistema(SistemaRequest sistema, RolTenantId rolId, CancellationToken cancellationToken)
    {
        var sistemaEncontrado = await _sistemaRepository.SistemaGetByIdAsync(new SistemaId(sistema.Id), cancellationToken);
        var sistemaDto = _mapper.Map<SistemaByRolDto>(sistemaEncontrado);

        
        await ProcessSistemaRecursive(sistemaDto, rolId, cancellationToken);
        

        RolPermisoTenant? rolPermiso = await _rolPermisoTenantRepository.GetByMenuAndRol(sistema.Id.ToString(), rolId, cancellationToken);

        if (sistemaDto.Completed != sistema.Completed)
        {
            if (rolPermiso == null && sistema.Completed)
            {
                rolPermiso = RolPermisoTenant.Create(rolId, sistema.Id.ToString());
                _rolPermisoTenantRepository.Add(rolPermiso);
            }
            else if (!sistema.Completed && rolPermiso != null)
            {
                _rolPermisoTenantRepository.Delete(rolPermiso);
            }
        }

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
                            var rolPermisoOpcion = await _rolPermisoOpcionTenantRepository.GetByOpcionAndRolPermiso(
                                rolPermiso.Id!, menuOpcion.OpcionId.ToString(), cancellationToken);

                            if (rolPermisoOpcion == null && menuOpcion.Completed)
                            {
                                var newRolPermisoOpcion = RolPermisoOpcionTenant.Create(
                                    rolPermiso.Id!,menuOpcion.OpcionId.ToString());
                                _rolPermisoOpcionTenantRepository.Add(newRolPermisoOpcion);
                            }
                            else if (!menuOpcion.Completed && rolPermisoOpcion != null)
                            {
                                _rolPermisoOpcionTenantRepository.Delete(rolPermisoOpcion);
                            }
                        }
                    }
                }
            }
        }

        if (sistema.Childrens.Count > 0)
        {
            foreach (var child in sistema.Childrens)
            {
                await ProcesarSistema(child, rolId, cancellationToken);
            }
        }
    }

     async Task ProcessSistemaRecursive(SistemaByRolDto sistema, RolTenantId rolId, CancellationToken cancellationToken)
    {
        var rolPermiso = await _rolPermisoTenantRepository.GetByMenuAndRol(sistema.Id!, rolId, cancellationToken);

        sistema.Completed = rolPermiso != null;

        var tieneRolPermiso = rolPermiso != null;

        foreach (var menuOpcion in sistema.MenuOpciones!)
        {
            menuOpcion.Completed = tieneRolPermiso
                ? await _rolPermisoOpcionTenantRepository.ValidarPermisoOpcion(rolPermiso!.Id!, menuOpcion.OpcionId!, cancellationToken)
                : false;
        }

        if (sistema.Childrens != null && sistema.Childrens.Count > 0)
        {
            foreach (var child in sistema.Childrens)
            {
                await ProcessSistemaRecursive(child, rolId, cancellationToken);
            }
        }
    }
}