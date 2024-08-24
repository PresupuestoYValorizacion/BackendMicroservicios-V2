using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Personas;

namespace MsAcceso.Domain.Root.PersonasJuridicas;

public interface IPersonaJuridicaRepository
{

    void Add(PersonaJuridica user);

    void Update(PersonaJuridica user);

}