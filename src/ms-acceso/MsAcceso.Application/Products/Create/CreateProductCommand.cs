using MsAcceso.Application.Abstractions.Messaging;


namespace MsAcceso.Application.Products.Create;

public sealed record CreateProductCommand(
    string Name,
    string Supplier
) : ICommand<string>;