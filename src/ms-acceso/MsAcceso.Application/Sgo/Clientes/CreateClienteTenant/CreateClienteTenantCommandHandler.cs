using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;

namespace MsAcceso.Application.Sgo.Clientes.CreateClienteTenant;

internal class CreateClienteTenantCommandHandler : ICommandHandler<CreateClienteTenantCommand, Guid>
{
    private readonly IClienteTenantRepository _clienteRepository;

    public CreateClienteTenantCommandHandler(
        IClienteTenantRepository clienteRepository
    )
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<Result<Guid>> Handle(CreateClienteTenantCommand request, CancellationToken cancellationToken)
    {
       var clienteExiste = await _clienteRepository.GetByNumeroDocumento(request.NumeroDocumento,cancellationToken);

        if(clienteExiste is not null)
        {
            return Result.Failure<Guid>(ClienteTenantErrors.ClienteExists);
        }

        var newCliente = ClienteTenant.Create(
                ClienteTenantId.New(),
                request.TipoPersonaId,
                request.TipoDocumentoId,
                request.TipoClienteId,
                request.NumeroDocumento,
                request.Nombre
        );

        _clienteRepository.Add(newCliente);

        await _clienteRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(newCliente.Id!.Value, Message.Create);

    }

}