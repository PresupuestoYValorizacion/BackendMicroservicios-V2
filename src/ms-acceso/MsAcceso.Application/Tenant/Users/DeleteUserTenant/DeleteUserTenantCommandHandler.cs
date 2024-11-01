using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Users.DeleteUserTenant;

internal sealed class DeleteUserTenantCommandHandler : ICommandHandler<DeleteUserTenantCommand, Guid>
{
    private readonly IUserTenantRepository _userRepository;
    private readonly ITenantProvider _tenantProvider;

    private readonly IPersonaTenantRepository _personaRepository;
    private readonly IPersonaNaturalTenantRepository _personaNaturalRepository;
    private readonly IPersonaJuridicaTenantRepository _personaJuridicaRepository;

    public DeleteUserTenantCommandHandler(
        IUserTenantRepository userRepository,
        IPersonaTenantRepository personaRepository, 
        IPersonaNaturalTenantRepository personaNaturalRepository, 
        IPersonaJuridicaTenantRepository personaJuridicaRepository,
        ITenantProvider tenantProvider
    )
    {
        _userRepository = userRepository;
        _personaRepository = personaRepository;
        _personaJuridicaRepository = personaJuridicaRepository;
        _personaNaturalRepository = personaNaturalRepository;
        _tenantProvider = tenantProvider;
    }

    public async Task<Result<Guid>> Handle(DeleteUserTenantCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var userDelete = await _userRepository.GetByIdUserIncludes(request.Id, cancellationToken);

            if (userDelete is null)
            {
                return Result.Failure<Guid>(UserTenantErrors.NotFound);
            }

            var persona = await _personaRepository.GetByIdAsync(userDelete.PersonaId!);

            if(userDelete.Persona!.TipoId == TipoPersona.Natural){

                _personaNaturalRepository.DeleteById(userDelete.PersonaId!);
            }else{
                _personaJuridicaRepository.DeleteById(userDelete.PersonaId!);
            }
            
            
            _personaRepository.Delete(persona!);

            _userRepository.Delete(userDelete);
            
            await _tenantProvider.Delete(request.Id.Value);

            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(request.Id.Value, Message.Delete);
        }
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<Guid>(UserTenantErrors.UserInUse);

        }
    }
}