using FluentValidation;

namespace MsAcceso.Application.Tenant.Roles.UpdateRoleTenant;

public sealed class UpdateRoleTenantCommandValidator : AbstractValidator<UpdateRoleTenantCommand>
{
    public UpdateRoleTenantCommandValidator()
    {
        RuleFor(o => o.Nombre).NotEmpty().WithMessage("El nombre del rol no puede ser nulo.")
                            .MaximumLength(100).WithMessage("El nombre del rol no puede tener más de 100 caracteres");

    }
}