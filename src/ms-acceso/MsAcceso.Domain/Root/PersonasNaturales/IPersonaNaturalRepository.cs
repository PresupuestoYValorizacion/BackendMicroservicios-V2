using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Personas;

namespace MsAcceso.Domain.Root.PersonasNaturales;

public interface IPersonaNaturalRepository
{


    void Add(PersonaNatural user);

    void Update(PersonaNatural user);


}