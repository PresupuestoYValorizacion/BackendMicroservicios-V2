using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.Sistemas.UpdateSistemas;

internal sealed class UpdateSistemasCommandHandler : ICommandHandler<UpdateSistemasCommand, Guid>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public UpdateSistemasCommandHandler(
        ISistemaRepository sistemaRepository,
        IUnitOfWorkApplication unitOfWorkTenant
    )
    {
        _sistemaRepository = sistemaRepository;
        _unitOfWork = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(UpdateSistemasCommand request, CancellationToken cancellationToken)
    {

        var sistemaExists = await _sistemaRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sistemaExists is null)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNotFound);
        }

        if(sistemaExists.Nombre != request.Nombre)
        {
            var nombreSistemaExists = await _sistemaRepository.SistemaExistsByName(request.Nombre!, cancellationToken);

            if (nombreSistemaExists )
            {
                return Result.Failure<Guid>(SistemaErrors.SistemaNotAvailable);
            }

        }

        if(sistemaExists.Url != request.Url)
        {
            var urlSistemaExists = await _sistemaRepository.SistemaExistsByUrl(request.Url!, cancellationToken);

            if (urlSistemaExists )
            {
                return Result.Failure<Guid>(SistemaErrors.SistemaUrlExists);
            }
        }

        if (request.EsIntercambio)
        {

            var sistemaIntercambio = await _sistemaRepository.GetByOrdenAsync(request.Orden, sistemaExists.Dependencia!, cancellationToken);

            if (sistemaIntercambio is null)
            {
                return Result.Failure<Guid>(SistemaErrors.SistemaIntercambioNotFound);
            }

            sistemaIntercambio.UpdateOrden(sistemaExists.Orden);

            _sistemaRepository.Update(sistemaIntercambio);

        }

        sistemaExists.Update(
            request.Nombre!,
            request.Logo!,
            request.Url!,
            request.Orden!
        );

        _sistemaRepository.Update(sistemaExists);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(sistemaExists.Id!.Value, Message.Update);
    }
}