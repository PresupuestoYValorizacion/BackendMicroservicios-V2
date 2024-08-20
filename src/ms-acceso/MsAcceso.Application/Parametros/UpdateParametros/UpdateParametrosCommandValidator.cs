using FluentValidation;

namespace MsAcceso.Application.Parametros.UpdateParametros;

public sealed class UpdateParametrosCommandValidator : AbstractValidator<UpdateParametrosCommand>
{
    public UpdateParametrosCommandValidator()
    {
        RuleFor(p => p.Nombre)
        .MaximumLength(50).WithMessage("El nombre del parametro no puede tener más de 50 caracteres");
        
        RuleFor(p => p.Abreviatura)
        .MaximumLength(10).WithMessage("La abreviatura del parametro no puede tener más de 10 caracteres");

        RuleFor(p => p.Abreviatura)
        .MaximumLength(300).WithMessage("La descripcion del parametro no puede tener más de 300 caracteres");

        RuleFor(p => p.Valor)
        .MaximumLength(2).WithMessage("El valor del parametro no puede tener más de 2 caracteres");
    }
}