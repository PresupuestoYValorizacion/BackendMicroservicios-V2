using FluentValidation;

namespace MsAcceso.Application.Tenant.Roles.RegisterRoleTenant;
public sealed class RegisterRolesCommandValidator : AbstractValidator<RegisterRoleTenantCommand>
{
    public RegisterRolesCommandValidator()
    {
        RuleFor(o => o.Nombre).NotEmpty().WithMessage("El nombre del rol no puede ser nulo.")
                            .MaximumLength(100).WithMessage("El nombre del rol no puede tener más de 100 caracteres");

    }
}