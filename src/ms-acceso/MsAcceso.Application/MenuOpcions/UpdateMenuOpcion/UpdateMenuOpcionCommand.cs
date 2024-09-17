using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.MenuOpcions.UpdateMenuOpcion;

public sealed record UpdateMenuOpcionCommand(
    MenuOpcionId MenuOpcionId,
    OpcionId OpcionId,
     bool TieneUrl,
    string Url,
    int Orden,
    bool EsIntercambio
) : ICommand<Guid>;