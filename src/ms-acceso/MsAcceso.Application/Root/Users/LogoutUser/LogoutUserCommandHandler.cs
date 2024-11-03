
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using AutoMapper;
using MsAcceso.Domain.Root.Sesiones;

namespace MsAcceso.Application.Root.Users.LogoutUser;

internal sealed class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand, LogoutUserResponse>
{

    private readonly ISesionRepository _sesionRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;
    private readonly IMapper _mapper;

    public LogoutUserCommandHandler(
        IUnitOfWorkApplication unitOfWork,
        ISesionRepository sesionRepository,
        IMapper mapper
    )
    {
        _sesionRepository = sesionRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<LogoutUserResponse>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {

        var sessionExistente = await _sesionRepository.GetByUserId(request.UserId, cancellationToken);


        if (sessionExistente == null)
        {

            return LogoutUserResponse.Create(request.IdTenant,request.IsTenant)!;
        }

        sessionExistente.Desactive();

        _sesionRepository.Update(sessionExistente);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return LogoutUserResponse.Create(request.IdTenant,request.IsTenant)!;



    }
}