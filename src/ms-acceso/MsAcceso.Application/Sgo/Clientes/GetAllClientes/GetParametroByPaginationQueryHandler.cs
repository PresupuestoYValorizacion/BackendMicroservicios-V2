using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Clientes.GetAllClientes;

internal sealed class GetAllClientesQueryHandler : IQueryHandler<GetAllClientesQuery, List<ClienteDto>>
{
    private readonly IClienteTenantRepository _clienteRepository;
    private readonly IMapper _mapper;

    public GetAllClientesQueryHandler(
        IClienteTenantRepository clienteRepository,
        IMapper mapper
    )
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ClienteDto>>> Handle( GetAllClientesQuery request, CancellationToken cancellationToken)
    {
        

        var clientes = await _clienteRepository.GetAllAsync(cancellationToken);

    
        var clientesDto = _mapper.Map<List<ClienteDto>>(clientes);

        return clientesDto!;
    }
}