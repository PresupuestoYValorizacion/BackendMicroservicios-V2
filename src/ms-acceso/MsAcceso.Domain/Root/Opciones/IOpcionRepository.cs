namespace MsAcceso.Domain.Root.Opciones;

public interface IOpcionRepository
{

    void Add(Opcion opcion);
    void Update(Opcion opcion);
    void Delete(Opcion opcion);
    Task<Opcion?> GetByIdAsync(OpcionId opcionId, CancellationToken cancellationToken = default);
    Task<bool> OpcionExist(string nombreOpcion, CancellationToken cancellationToken = default);
}