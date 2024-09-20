using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Users.ValidarAccesoMenu;

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

        string url = request.Url!;

        url = url.Trim('/');

        bool containsHttp = url.Contains("http");

        if (!containsHttp)
        {
            string[] partesUrl = url!.Split('/');

            url = "/" + partesUrl[0];
        }

        var sistema = await _sistemaRepository.GetByUrlAsync(url, request.RolId!, cancellationToken);

        if (sistema is null)
        {
            return Result.Failure<bool>(SistemaErrors.SistemaNotFound);
        }

        bool existePermiso = false;

        if (!containsHttp)
        {
            string[] partesUrl = request.Url!.Trim('/').Split('/');
            existePermiso = VerificarPermisoRecursivo(sistema, request.RolId!, partesUrl, 0);

        }
        else
        {
            existePermiso = sistema.RolPermisos!.Any(x => x.RolId == request.RolId!);

        }


        return existePermiso;

    }

    private static bool VerificarPermisoRecursivo(Sistema sistema, RolId rolId, string[] partesUrl, int nivelActual)
    {
        bool tienePermiso = sistema.RolPermisos!.Any(x => x.RolId == rolId);

        if (!tienePermiso)
        {
            return false;
        }

        if (nivelActual == partesUrl.Length - 1)
        {
            return true;
        }

        string siguienteParte = "/" + partesUrl[nivelActual +1];

        var subsistema = sistema.Sistemas?.FirstOrDefault(x => x.Url == siguienteParte);
        if (subsistema != null)
        {
            return VerificarPermisoRecursivo(subsistema, rolId, partesUrl, nivelActual + 1);
        }

        var opcion = sistema.MenuOpcions?.FirstOrDefault(x => x.Url == siguienteParte);
        if (opcion != null)
        {
            
            return true;
        }

        return false;
    }

}