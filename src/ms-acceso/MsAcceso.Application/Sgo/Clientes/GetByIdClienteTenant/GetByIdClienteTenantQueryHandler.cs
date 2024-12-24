using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Clientes.GetByIdClienteTenant;

internal sealed class GetByIdClienteTenantQueryHandler : IQueryHandler<GetByIdClientTenantQuery, ClienteDto>
{
    private readonly IClienteTenantRepository _clienteRepository;

    private readonly IMapper _mapper;

    public GetByIdClienteTenantQueryHandler(
        IClienteTenantRepository clienteRepository,
        IMapper mapper
    )
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task<Result<ClienteDto>> Handle(GetByIdClientTenantQuery request, CancellationToken cancellationToken)
    {
        
        var cliente = await _clienteRepository.GetByIdAsync(new ClienteTenantId( Guid.Parse(request.Id)), cancellationToken);

        if(cliente is null){
            return Result.Failure<ClienteDto>(ClienteTenantErrors.ClienteNotFound)!;
        }

        var clienteDto = _mapper.Map<ClienteDto>(cliente);

        return clienteDto!;
        
    }
}