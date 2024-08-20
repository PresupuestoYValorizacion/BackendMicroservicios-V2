using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Parametros.UpdateParametros;

internal sealed class UpdateParametrosCommandHandler : ICommandHandler<UpdateParametrosCommand, int>
{
    private readonly IParametroRepository _parametroRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public UpdateParametrosCommandHandler(
        IParametroRepository parametroRepository,
        IUnitOfWorkTenant unitOfWork)
        {
            _parametroRepository = parametroRepository;
            _unitOfWork = unitOfWork;
        }

    public async Task<Result<int>> Handle(UpdateParametrosCommand request, CancellationToken cancellationToken)
    {

        var parametro = await _parametroRepository.GetByIdAsync(request.Id,cancellationToken);

        if(parametro is null)
        {
            return Result.Failure<int>(ParametroErrors.ParametroNotFound);
        }

        if(parametro.Nombre != request.Nombre){

            var parametroExists = await _parametroRepository.ParametroExists(request.Nombre!,parametro.Nivel!.Value,cancellationToken); 

            if(parametroExists)
            {
                return Result.Failure<int>(ParametroErrors.ParametroExists);
            }
        }


        if(request.Nombre is null || request.Abreviatura is null || request.Descripcion is null)
        {
            return Result.Failure<int>(Error.NullValue);
        }

        var valorExist = false;

        if(request.Valor!.Length >0 && parametro.Nivel!.Value > 0){

            valorExist = await _parametroRepository.ValorExists(request.Valor, parametro.Dependencia!.Value, cancellationToken);
        }

        if(valorExist){
            return Result.Failure<int>(ParametroErrors.ValorExists);
        }


        parametro.Update(
            request.Nombre,
            request.Abreviatura,
            request.Descripcion,
            request.Valor!);

        _parametroRepository.Update(parametro);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(parametro.Id!.Value, Message.Update);
    }
}