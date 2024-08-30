using FluentValidation;

namespace MsAcceso.Application.Productos.UpdateProductos;

public sealed class UpdateProductoCommandValidator : AbstractValidator<UpdateProductoCommand>
{

    public UpdateProductoCommandValidator()
    {
        RuleFor(o => o.Nombre)
            .NotEmpty().WithMessage("El nombre del producto no puede ser nulo")
            .MaximumLength(50).WithMessage("El nombre del producto no puede tener más de 50 caracteres");
            
        RuleFor(o => o.Codigo)
            .NotEmpty().WithMessage("El codigo del producto no puede ser nulo")
            .MaximumLength(50).WithMessage("El codigo del producto no puede tener más de 50 caracteres");
        
        RuleFor(o => o.Cantidad)
            .GreaterThan(0).WithMessage("El valor de la cantidad no puede ser menor a 0");
    }
}