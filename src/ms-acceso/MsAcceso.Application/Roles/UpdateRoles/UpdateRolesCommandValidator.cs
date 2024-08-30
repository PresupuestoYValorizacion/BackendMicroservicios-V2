using FluentValidation;

namespace MsAcceso.Application.Roles.UpdateRoles;

public sealed class UpdateRolesCommandValidator : AbstractValidator<UpdateRolesCommand>
{
    public UpdateRolesCommandValidator()
    {
        RuleFor(o => o.Nombre).NotEmpty().WithMessage("El nombre del rol no puede ser nulo.")
                            .MaximumLength(100).WithMessage("El nombre del rol no puede tener más de 100 caracteres");

        RuleFor(o => o.TipoRolId).NotEmpty().WithMessage("El tipo de rol no puede ser nulo.");
    }
}