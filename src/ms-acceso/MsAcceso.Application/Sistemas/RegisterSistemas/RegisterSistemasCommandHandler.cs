using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.RegisterSistemas;

internal sealed class RegisterSistemasCommandHandler : ICommandHandler<RegisterSistemasCommand, Guid>
{

    private readonly ISistemaRepository _sistemaRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public RegisterSistemasCommandHandler(
        ISistemaRepository sistemaRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _sistemaRepository = sistemaRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(RegisterSistemasCommand request, CancellationToken cancellationToken)
    {

        if (request.Nombre is null || request.Logo is null || request.Url is null)
        {
            return Result.Failure<Guid>(Error.NullValue);
        }

        var nombreSistema = request.Nombre;
        var nombreSistemaExists = await _sistemaRepository.SistemaExistsByName(nombreSistema, cancellationToken);

        if(nombreSistemaExists)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNameExists);
        }

        var urlSistema = request.Url;
        var urlSistemaExists = await _sistemaRepository.SistemaExistsByUrl(urlSistema, cancellationToken);

        if(urlSistemaExists)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaUrlExists);
        }

        if(request.Dependecia is null || request.Dependecia == ""){

            var sistema = Sistema.Create(
                null,
                nombreSistema,
                request.Logo,
                0,
                request.Url
            );

            _sistemaRepository.Add(sistema);
            await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

            return Result.Success(sistema.Id!.Value, Message.Create);
        }else{

            var idDependencia = Guid.Parse(request.Dependecia);
            var sistemaDependencia = await _sistemaRepository.GetByIdAsync(new SistemaId(idDependencia), cancellationToken);

            if(sistemaDependencia is null)
            {
                return Result.Failure<Guid>(SistemaErrors.SistemaNotAvailable);
            }

            var nivel = sistemaDependencia.Nivel + 1; 

            var sistema = Sistema.Create(
                sistemaDependencia.Id,
                nombreSistema,
                request.Logo,
                nivel,
                request.Url
            );

            _sistemaRepository.Add(sistema);
            await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

            return Result.Success(sistema.Id!.Value, Message.Create);
        }
    }
}