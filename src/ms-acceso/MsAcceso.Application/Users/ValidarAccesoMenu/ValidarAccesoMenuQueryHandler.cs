using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.RolPermisos;
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

        if(!url.Contains("http"))
        {
            string[] partesUrl = url!.Split('/');

            url = "/"+ partesUrl[0];
        }

        //TODO VALIDAR CON EL PARTES URL SI EXISTE LA OPCION PERO PRIMERO VERIFICAR SI NO ES UN SUBNIVEL DE
        //TODO REPENTE SE HACE UNA FUNCION RECURISVA 


        var sistema = await _sistemaRepository.GetByUrlAsync(url,request.RolId!, cancellationToken);
        

        if(sistema is null)
        {
            return Result.Failure<bool>(SistemaErrors.SistemaNotFound);
        }

        var existePermiso = sistema.RolPermisos!.Any(x => x.RolId == request.RolId);

        return existePermiso;

    }


}