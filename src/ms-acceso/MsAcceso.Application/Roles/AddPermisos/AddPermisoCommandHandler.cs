using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.RolPermisosOpciones;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Roles.AddPermisos;

internal sealed class AddPermisosCommandHandler : ICommandHandler<AddPermisosCommand, Guid>
{

    private readonly IMapper _mapper;
    private readonly IRolRepository _rolRepository;
    private readonly ISistemaRepository _sistemaRepository;

    private readonly IRolPermisoRepository _rolPermisoRepository;
    private readonly IRolPermisoOpcionRepository _rolPermisoOpcionRepository;

    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public AddPermisosCommandHandler(
        IMapper mapper,
        IRolRepository rolRepository,
        ISistemaRepository sistemaRepository,
        IRolPermisoOpcionRepository rolPermisoOpcionRepository,
        IRolPermisoRepository rolPermisoRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _mapper = mapper;
        _rolRepository = rolRepository;
        _rolRepository = rolRepository;
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

                var sistemaEncontrado = await _sistemaRepository.GetSistemaByIdAndRol(request.RolId, new SistemaId(sistema.Id), cancellationToken);

                var sistemaDto = _mapper.Map<SistemaByRolDto>(sistemaEncontrado);

                RolPermiso? rolPermiso = await _rolPermisoRepository.GetByMenuAndRol(new SistemaId(sistema.Id), request.RolId);

                if (sistemaDto.Completed != sistema.Completed)
                {

                    if (rolPermiso is null && sistema.Completed)
                    {
                        rolPermiso = RolPermiso.Create(request.RolId, new SistemaId(sistema.Id));

                        _rolPermisoRepository.Add(rolPermiso);

                    }

                    if (!sistema.Completed && rolPermiso is not null)
                    {
                        _rolPermisoRepository.Delete(rolPermiso);
                    }

                }

                if (sistema.Completed && rolPermiso is not null)
                {

                    if (sistema.MenuOpciones.Count > 0)
                    {
                        foreach (var menuOpcion in sistema.MenuOpciones)
                        {

                            var menuOpcionDto = sistemaDto.MenuOpciones!
                                                .FirstOrDefault(mo => mo.OpcionId == menuOpcion.OpcionId.ToString());

                            if (menuOpcionDto != null)
                            {
                                // Comparar el estado 'Completed' de cada MenuOpcion
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

                                    if (!menuOpcion.Completed && rolPermisoOpcion != null)
                                    {
                                        _rolPermisoOpcionRepository.Delete(rolPermisoOpcion);
                                    }
                                }
                            }
                        }
                    }


                }


            }

        }


        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(request.RolId.Value, Message.Update);

    }
}