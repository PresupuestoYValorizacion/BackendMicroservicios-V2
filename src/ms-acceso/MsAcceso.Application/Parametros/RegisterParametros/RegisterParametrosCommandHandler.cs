using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Parametros.RegisterParametros;

internal sealed class RegisterParametrosCommandHandler : ICommandHandler<RegisterParametrosCommand, int>
{
    private readonly IParametroRepository _parametroRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public RegisterParametrosCommandHandler(
        IParametroRepository parametroRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _parametroRepository = parametroRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(RegisterParametrosCommand request, CancellationToken cancellationToken)
    {

        if(request.Nombre is null || request.Abreviatura is null || request.Descripcion is null)
        {
            return Result.Failure<int>(Error.NullValue);
        }

        var nombre = request.Nombre;

        var parametroExists = await _parametroRepository.ParametroExists(nombre,request.Nivel, cancellationToken); 

        if(parametroExists)
        {
            return Result.Failure<int>(ParametroErrors.ParametroExists);
        }

        var lastId = await _parametroRepository.GetLastParametroIdAsync(cancellationToken);
        var newId = lastId+1;

        //*Validar si Dependencia existe

        var dependenciaExists = Parametro.New();

        if(request.Dependencia != 0)
        {
            dependenciaExists = await _parametroRepository.GetByIdAsync(new ParametroId(request.Dependencia),cancellationToken);
        }

        if(dependenciaExists is null)
        {
            return Result.Failure<int>(ParametroErrors.DependenciaNotFound);
        }

        //*Obtener nivel

        var nivel = 0;

        if(request.Nivel != 0)
        {
            nivel = request.Nivel;
        }

        var valorExist = false;

        if(request.Valor.Length >0 && nivel > 0){

            valorExist = await _parametroRepository.ValorExists(request.Valor, request.Dependencia, cancellationToken);

        }

        if(valorExist){
            return Result.Failure<int>(ParametroErrors.ValorExists);
        }

        var parametro = Parametro.Create(
            new ParametroId(newId),
            nombre,
            request.Abreviatura,
            request.Descripcion,
            (request.Dependencia != 0) ? new ParametroId(request.Dependencia) : null,
            nivel,
            request.Valor
        );

        _parametroRepository.Add(parametro);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(parametro.Id!.Value, Message.Create);
    }
}