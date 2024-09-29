using FluentValidation;

namespace MsAcceso.Application.Root.Roles.RegisterRoles;

public sealed class RegisterRolesCommandValidator : AbstractValidator<RegisterRolesCommand>
{
    public RegisterRolesCommandValidator()
    {
        RuleFor(o => o.Nombre).NotEmpty().WithMessage("El nombre del rol no puede ser nulo.")
            .MaximumLength(100).WithMessage("El nombre del rol no puede tener más de 100 caracteres");

        RuleFor(o => o.TipoRolId).NotEmpty().WithMessage("El tipo de rol no puede ser nulo.");
    }
}