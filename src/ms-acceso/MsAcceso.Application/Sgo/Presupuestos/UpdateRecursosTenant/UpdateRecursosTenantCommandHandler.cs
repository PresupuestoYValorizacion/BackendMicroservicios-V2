
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.UpdateRecursosTenant;

internal class UpdateRecursosTenantCommandHandler : ICommandHandler<UpdateRecursosTenantCommand, Guid>
{
    private readonly IRecursoTenantRepository _recursoRepository;
    private readonly IPartidaRecursoTenantRepository _partidaRecursoRepository;
    private readonly IPartidaTenantRepository _partidaRepository;

    public UpdateRecursosTenantCommandHandler(
        IPartidaRecursoTenantRepository partidaRecursoRepository,
        IRecursoTenantRepository recursoRepository,
        IPartidaTenantRepository partidaRepository
    )
    {
        _partidaRecursoRepository = partidaRecursoRepository;
        _recursoRepository = recursoRepository;
        _partidaRepository = partidaRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateRecursosTenantCommand request, CancellationToken cancellationToken)
    {
        var partidaRecursoExiste = await _partidaRecursoRepository.GetByIdAsync(new PartidaRecursoTenantId(Guid.Parse(request.Id)), cancellationToken);
        
         if(partidaRecursoExiste is null)
        {
            return Result.Failure<Guid>(PartidaRecursoTenantErrors.NotFound);
        }
        
        var partidaExiste = await _partidaRepository.GetByIdAsync(new PartidaTenantId(Guid.Parse(request.PartidaId)), cancellationToken);

        if(partidaExiste is null)
        {
            return Result.Failure<Guid>(PartidaTenantErrors.PartidaNotFound);
        }

        var recursoExiste = await _recursoRepository.GetByIdAsync(new RecursoTenantId(Guid.Parse(request.RecursoId)), cancellationToken);

        if(recursoExiste is  null)
        {
            return Result.Failure<Guid>(RecursoTenantErrors.NotFound);
        }

        partidaRecursoExiste.Update(partidaExiste!.Id!, recursoExiste!.Id!, request.Cantidad, request.Cuadrilla, request.Precio, request.Cantidad * request.Precio);
        
        _partidaRecursoRepository.Update(partidaRecursoExiste);

        await _partidaRecursoRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(Guid.Empty, Message.Create);

    }


}