using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Tenant.UsersTenant;


namespace MsSgo.Application.Clientes.CreateClienteTenant;

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

        if(clienteExiste == null)
        {
            return Result.Failure<Guid>(ClienteTenantErrors.ClienteExists);
        }

        var newCliente = ClienteTenant.Create(
                ClienteTenantId.New(),
                request.TipoPersonaId,
                request.TipoDocumentoId,
                request.NumeroDocumento,
                request.Nombre
        );

        _clienteRepository.Add(newCliente);

        await _clienteRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(newCliente.Id!.Value, Message.Create);

    }

}