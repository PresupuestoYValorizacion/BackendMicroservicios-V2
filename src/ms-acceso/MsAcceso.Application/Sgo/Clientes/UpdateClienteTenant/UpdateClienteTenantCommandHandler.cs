using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Clientes.UpdateClienteTenant;

internal sealed class UpdateClienteTenantCommandHandler : ICommandHandler<UpdateClienteTenantCommand, Guid>
{
    private readonly IClienteTenantRepository _clienteTenantRepository;

    public UpdateClienteTenantCommandHandler(
        IClienteTenantRepository clienteTenantRepository)
        {
            _clienteTenantRepository = clienteTenantRepository;
        }

    public async Task<Result<Guid>> Handle(UpdateClienteTenantCommand request, CancellationToken cancellationToken)
    {

        var cliente = await _clienteTenantRepository.GetByIdAsync(new ClienteTenantId(Guid.Parse(request.Id)),cancellationToken);

        if(cliente is null)
        {
            return Result.Failure<Guid>(ClienteTenantErrors.ClienteNotFound);
        }

        if(cliente.NumeroDocumento != request.NumeroDocumento){

            var numeroDocumentoExists = await _clienteTenantRepository.GetByNumeroDocumento(request.NumeroDocumento!,cancellationToken); 

            if(numeroDocumentoExists is not null)
            {
                return Result.Failure<Guid>(ClienteTenantErrors.ClienteExists);
            }
        }

        cliente.Update(
            request.TipoPersonaId,
            request.TipoDocumentoId,
            request.TipoClienteId,
            request.NumeroDocumento,
            request.Nombre);

        _clienteTenantRepository.Update(cliente);

        await _clienteTenantRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(cliente.Id!.Value, Message.Update);
    }
}