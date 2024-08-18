

using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Abstractions;
public abstract class Entity<TEntityId> : IEntity
{
    protected Entity()
    {

    }
    private readonly List<IDomainEvent> _domainEvents = new();
    
    protected Entity(TEntityId id)
    {
        Id = id;
        Activo = new Activo(true);
    }
    
    public TEntityId? Id {get ; init;}

    public Activo? Activo {get; set;}

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }


}