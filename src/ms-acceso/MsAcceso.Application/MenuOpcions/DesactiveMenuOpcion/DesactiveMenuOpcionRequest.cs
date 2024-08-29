using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.MenuOpcions.DesactiveMenuOpcions;

public record DesactiveMenuOpcionRequest(
    string MenuId,
    string OpcionId   
);