using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.Sistemas.RegisterSistemas;

internal sealed class RegisterSistemasCommandHandler : ICommandHandler<RegisterSistemasCommand, Guid>
{

    private readonly ISistemaRepository _sistemaRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public RegisterSistemasCommandHandler(
        ISistemaRepository sistemaRepository,
        IUnitOfWorkApplication unitOfWorkTenant
    )
    {
        _sistemaRepository = sistemaRepository;
        _unitOfWork = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(RegisterSistemasCommand request, CancellationToken cancellationToken)
    {

        if (request.Nombre is null || request.Logo is null || request.Url is null)
        {
            return Result.Failure<Guid>(Error.NullValue);
        }

        var nombreSistema = request.Nombre;
        var nombreSistemaExists = await _sistemaRepository.SistemaExistsByName(nombreSistema, cancellationToken);

        if (nombreSistemaExists)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNameExists);
        }

        var urlSistema = request.Url;


        var dependencia = request.Dependecia != null ? new SistemaId(new Guid(request.Dependecia)) : null;
    
        var urlSistemaExists = await _sistemaRepository.SistemaExistsByUrl(urlSistema, cancellationToken);

        if (urlSistemaExists)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaUrlExists);
        }

        var orden = await _sistemaRepository.GetCountSistemasByDependencia(dependencia, cancellationToken);

        orden = (orden == 0) ? 1 : (orden + 1);

        var sistema = Sistema.Create(
                dependencia,
                nombreSistema,
                request.Logo,
                request.Nivel,
                orden,
                request.Url
            );

        _sistemaRepository.Add(sistema);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(sistema.Id!.Value, Message.Create);

    }
}