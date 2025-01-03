

using MsAcceso.Application.Abstractions.Messaging;

using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Domain.Root.PersonasNaturales;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Users.DeleteUser;

internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    private readonly IPersonaRepository _personaRepository;
    private readonly IPersonaNaturalRepository _personaNaturalRepository;
    private readonly IPersonaJuridicaRepository _personaJuridicaRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        IPersonaRepository personaRepository, 
        IPersonaNaturalRepository personaNaturalRepository, 
        IPersonaJuridicaRepository personaJuridicaRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _userRepository = userRepository;
        _personaRepository = personaRepository;
        _personaJuridicaRepository = personaJuridicaRepository;
        _personaNaturalRepository = personaNaturalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userDelete = await _userRepository.GetByIdUserIncludes(request.Id, cancellationToken);

        if (userDelete is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var persona = await _personaRepository.GetByIdAsync(userDelete.EmpresaId!);

        if(userDelete.Empresa!.TipoId == new ParametroId(TipoPersona.Natural)){

            _personaNaturalRepository.DeleteById(userDelete.EmpresaId!);
        }else{
            _personaJuridicaRepository.DeleteById(userDelete.EmpresaId!);
        }
        
        _personaRepository.Delete(persona!);

        _userRepository.Delete(userDelete);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(request.Id.Value, Message.Delete);
    }
}