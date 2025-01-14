
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.CreateRecursosTenant;

internal class CreateRecursosTenantCommandHandler : ICommandHandler<CreateRecursosTenantCommand, Guid>
{
    // private readonly IPresupuestoEspecialidadTituloTenantRepository _especialidadTituloRepository;
    // private readonly ITituloTenantRepository _tituloRepository;
    private readonly IRecursoTenantRepository _recursoRepository;
    private readonly IPartidaRecursoTenantRepository _partidaRecursoRepository;
    private readonly IPartidaTenantRepository _partidaRepository;

    public CreateRecursosTenantCommandHandler(
        IPartidaRecursoTenantRepository partidaRecursoRepository,
        IRecursoTenantRepository recursoRepository,
        IPartidaTenantRepository partidaRepository
    )
    {
        _partidaRecursoRepository = partidaRecursoRepository;
        _recursoRepository = recursoRepository;
        _partidaRepository = partidaRepository;
    }

    public async Task<Result<Guid>> Handle(CreateRecursosTenantCommand request, CancellationToken cancellationToken)
    {
        var partidaExiste = await _partidaRepository.GetByIdAsync(new PartidaTenantId(Guid.Parse(request.PartidaId)), cancellationToken);

        if(partidaExiste is not null)
        {
            return Result.Failure<Guid>(PartidaTenantErrors.PartidaNotFound);
        }

        var recursoExiste = await _recursoRepository.GetByIdAsync(new RecursoTenantId(Guid.Parse(request.RecursoId)), cancellationToken);

        if(recursoExiste is not null)
        {
            return Result.Failure<Guid>(RecursoTenantErrors.NotFound);
        }

        var newPartidaRecurso = PartidaRecursoTenant.Create(partidaExiste!.Id!, recursoExiste!.Id!, request.Cantidad, request.Cuadrilla, request.Precio, 0);
        
        _partidaRecursoRepository.Add(newPartidaRecurso);

        await _partidaRecursoRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(Guid.Empty, Message.Create);

    }


}