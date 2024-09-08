using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Parametros.GetMenusByUser;

internal sealed class GetMenusByUserQueryHandler : IQueryHandler<GetMenusByUserQuery, List<SistemaByRolDto>>
{
    private readonly IRolPermisoRepository _rolPermisoRepository;
    private readonly IRolRepository _rolRepository;
    private readonly ISistemaRepository _sistemaRepository;

    private readonly IMapper _mapper;

    public GetMenusByUserQueryHandler(
        ISistemaRepository sistemaRepository,
        IRolPermisoRepository rolPermisoRepository,
        IRolRepository rolRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _rolPermisoRepository = rolPermisoRepository;
        _rolRepository = rolRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<SistemaByRolDto>>> Handle(GetMenusByUserQuery request, CancellationToken cancellationToken)
    {
        SistemaId? dependencia = request.Dependencia != null ? new SistemaId(new Guid(request.Dependencia)) : null;

        var sistemas = await _sistemaRepository.GetAllSistemasByRol(request.RolId!, cancellationToken);

        var sistemasDto = _mapper.Map<List<SistemaByRolDto>>(sistemas);

        List<SistemaByRolDto> sistemasFiltrados;
        if (dependencia != null)
        {
            sistemasFiltrados = BuscarPorDependenciaYEstado(sistemasDto, dependencia.ToString()!);
        }
        else
        {
            sistemasFiltrados = FiltrarCompletados(sistemasDto); 
        }

        return sistemasFiltrados!;
    }

    public List<SistemaByRolDto> FiltrarCompletados(List<SistemaByRolDto> sistemasDto)
    {
        var result = new List<SistemaByRolDto>();

        void FiltrarRecursivo(SistemaByRolDto sistema)
        {
            if (sistema.Completed)
            {
                var sistemaFiltrado = new SistemaByRolDto
                {
                    Id = sistema.Id,
                    Nombre = sistema.Nombre,
                    Dependencia = sistema.Dependencia,
                    Logo = sistema.Logo,
                    Nivel = sistema.Nivel,
                    Url = sistema.Url,
                    Completed = sistema.Completed,
                    MenuOpciones = sistema.MenuOpciones, // Puedes filtrar `MenuOpciones` si también aplicas el filtro aquí
                    Childrens = sistema.Childrens != null ? FiltrarCompletados(sistema.Childrens) : null // Filtrar recursivamente los hijos
                };
                result.Add(sistemaFiltrado);
            }
        }

        foreach (var sistema in sistemasDto)
        {
            FiltrarRecursivo(sistema);
        }

        return result;
    }


    public List<SistemaByRolDto> BuscarPorDependenciaYEstado(List<SistemaByRolDto> sistemasDto, string dependencia)
    {
        var result = new List<SistemaByRolDto>();

        void BuscarRecursivo(SistemaByRolDto sistema)
        {
            if (sistema.Dependencia == dependencia && sistema.Completed)
            {
                var sistemaFiltrado = new SistemaByRolDto
                {
                    Id = sistema.Id,
                    Nombre = sistema.Nombre,
                    Dependencia = sistema.Dependencia,
                    Logo = sistema.Logo,
                    Nivel = sistema.Nivel,
                    Url = sistema.Url,
                    Completed = sistema.Completed,
                    MenuOpciones = sistema.MenuOpciones, // Puedes filtrar `MenuOpciones` si también lo necesitas
                    Childrens = sistema.Childrens != null ? FiltrarCompletados(sistema.Childrens) : null // Filtrar recursivamente los hijos
                };
                result.Add(sistemaFiltrado);
            }

            if (sistema.Childrens != null)
            {
                foreach (var child in sistema.Childrens)
                {
                    BuscarRecursivo(child);
                }
            }
        }

        foreach (var sistema in sistemasDto)
        {
            BuscarRecursivo(sistema);
        }

        return result;
    }



}