namespace MsAcceso.Domain.Root.Ciudadanos;

public record CiudadanoId(Guid Value){

    public static CiudadanoId New() => new(Guid.NewGuid());

};