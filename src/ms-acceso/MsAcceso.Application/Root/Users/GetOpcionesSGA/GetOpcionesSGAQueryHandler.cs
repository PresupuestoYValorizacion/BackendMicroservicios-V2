using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.Users.GetOpcionesSGA;

internal sealed class GetOpcionesSGAQueryHandler : IQueryHandler<GetOpcionesSGAQuery, List<MenuOpcionDto>>
{
    private readonly ISistemaRepository _sistemaRepository;

    private readonly IMapper _mapper;

    public GetOpcionesSGAQueryHandler(
        ISistemaRepository sistemaRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<MenuOpcionDto>>> Handle(GetOpcionesSGAQuery request, CancellationToken cancellationToken)
    {
        string url = request.Url!;

        var sistema = await _sistemaRepository.GetByUrlAsync(url,request.RolId!, cancellationToken);

         if(sistema is null)
        {
            return Result.Failure<List<MenuOpcionDto>>(SistemaErrors.SistemaNotFound)!;
        }

        var menuOpcionDtos = _mapper.Map<List<MenuOpcionDto>>(sistema!.MenuOpcions);

        var menuOpcionesCompletadas = menuOpcionDtos.Where(m => m.Completed == true).OrderBy(x => x.Orden).ToList();

        return menuOpcionesCompletadas!;
    }



}