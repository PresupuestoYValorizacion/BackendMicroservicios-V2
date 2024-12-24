using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.Users.GetOpcionesSGO;

internal sealed class GetOpcionesSGOQueryHandler : IQueryHandler<GetOpcionesSGOQuery, List<MenuOpcionDto>>
{
    private readonly ISistemaRepository _sistemaRepository;

    private readonly IMapper _mapper;

    public GetOpcionesSGOQueryHandler(
        ISistemaRepository sistemaRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<MenuOpcionDto>>> Handle(GetOpcionesSGOQuery request, CancellationToken cancellationToken)
    {
        string url = request.Url!;

        int lastIndex = url.LastIndexOf('/');
        string lastSegment  = string.Empty;
       
        if (lastIndex != -1)
        {
            lastSegment = "/" +url.Substring(lastIndex + 1);
        }

        var sistema = await _sistemaRepository.GetByUrlAsync(lastSegment,request.RolId!, cancellationToken);

         if(sistema is null)
        {
            return Result.Failure<List<MenuOpcionDto>>(SistemaErrors.SistemaNotFound)!;
        }

        var menuOpcionDtos = _mapper.Map<List<MenuOpcionDto>>(sistema!.MenuOpcions);

        var menuOpcionesCompletadas = menuOpcionDtos.Where(m => m.Completed == true).OrderBy(x => x.Orden).ToList();

        return menuOpcionesCompletadas!;
    }



}