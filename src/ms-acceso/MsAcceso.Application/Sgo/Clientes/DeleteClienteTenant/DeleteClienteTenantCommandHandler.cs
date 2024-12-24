using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Clientes.DeleteClienteTenant;

internal sealed class DeleteClienteTenantCommandHandler : ICommandHandler<DeleteClienteTenantCommand, int>
{
    private readonly IClienteTenantRepository _clienteTenantRepository;

    public DeleteClienteTenantCommandHandler(
        IClienteTenantRepository clienteTenantRepository
    )
    {
        _clienteTenantRepository = clienteTenantRepository;
    }

    public async Task<Result<int>> Handle(DeleteClienteTenantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var clienteDelete = await _clienteTenantRepository.GetByIdAsync(request.Id, cancellationToken);

            if (clienteDelete is null)
            {
                return Result.Failure<int>(ClienteTenantErrors.ClienteNotFound);
            }

            _clienteTenantRepository.Delete(clienteDelete);
            
            await _clienteTenantRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(1, Message.Delete);

        }
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<int>(ClienteTenantErrors.ClienteInUse);

        }

    }
}