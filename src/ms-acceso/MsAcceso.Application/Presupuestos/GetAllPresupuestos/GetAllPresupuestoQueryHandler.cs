using AutoMapper;
using MediatR;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Tenant.Presupuestos;


namespace MsAcceso.Application.Presupuestos.GetAllPresupuestos;


internal sealed class GetAllPresupuestosQueryHandler : IQueryHandler<GetAllPresupuestosQuery, List<Presupuesto>>
{
    private readonly IPresupuestoTenantRepository _presupuestoRepository;
    private readonly IPruebaTenantRepository _pruebaRepository;

    private readonly IMapper _mapper;

    public GetAllPresupuestosQueryHandler(
        IPresupuestoTenantRepository presupuestoRepository,
        IPruebaTenantRepository pruebaRepository,
        IMapper mapper
    )
    {
        _presupuestoRepository = presupuestoRepository;
        _pruebaRepository = pruebaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<Presupuesto>>> Handle(GetAllPresupuestosQuery request, CancellationToken cancellationToken)
    {
        var presupuestos = await _presupuestoRepository.GetAll(cancellationToken);

        var pruebas = await _pruebaRepository.GetAll(cancellationToken);


        return presupuestos!;

    }

}