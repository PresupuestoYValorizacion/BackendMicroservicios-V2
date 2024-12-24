using FluentValidation;

namespace MsAcceso.Application.Sgo.Clientes.UpdateClienteTenant;

public sealed class UpdateClienteTenantCommandValidator : AbstractValidator<UpdateClienteTenantCommand>
{
    public UpdateClienteTenantCommandValidator()
    {
        RuleFor(p => p.Nombre)
        .MaximumLength(100).WithMessage("El nombre del cliente no puede tener más de 100 caracteres");
        
    }
}