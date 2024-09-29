using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Root.MenuOpcions.UpdateMenuOpcion;

public sealed record UpdateMenuOpcionCommand(
    MenuOpcionId MenuOpcionId,
    OpcionId OpcionId,
    bool TieneUrl,
    string Url,
    int Orden,
    bool EsIntercambio
) : ICommand<Guid>;