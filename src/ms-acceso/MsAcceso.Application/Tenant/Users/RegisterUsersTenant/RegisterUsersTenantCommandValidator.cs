using FluentValidation;

namespace MsAcceso.Application.Tenant.Users.RegisterUsersTenant;

public sealed class RegisterUsersTenantCommandValidator : AbstractValidator<RegisterUsersTenantCommand>
{
    public RegisterUsersTenantCommandValidator()
    {
        RuleFor(c => c.Username).NotEmpty().WithMessage("El username no puede ser nulo");

        RuleFor(c => c.Email).EmailAddress();
        
        RuleFor(c => c.Password).NotEmpty().MinimumLength(5);

        // RuleFor(c => c.EmpresaId)
        //         .NotEmpty()
        //         .Must(id => Guid.TryParse(id, out _))
        //         .WithMessage("La EmpresaId no puede ser un GUID vacío");


    }
}