using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Libros;

namespace MsAcceso.Application.Root.Libros.RegisterLibros;

internal class RegisterLibroCommandHandler : ICommandHandler<RegisterLibroCommand , Guid>
{

    private readonly ILibroRepository _libroRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public RegisterLibroCommandHandler(
        ILibroRepository libroRepository,
        IUnitOfWorkApplication unitOfWork
    )
    {
        _libroRepository = libroRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterLibroCommand request, CancellationToken cancellationToken)
    {
        var libroExists = await _libroRepository.LibroExist(request.Nombre,cancellationToken);

        if(libroExists){
            return Result.Failure<Guid>(LibroErrors.LibroExists);
        }
        
        var libro = Libro.Create(
            request.Nombre,
            request.Descripcion,
            request.Precio
        );

        _libroRepository.Add(libro);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(libro.Id!.Value, Message.Create);
    }
}