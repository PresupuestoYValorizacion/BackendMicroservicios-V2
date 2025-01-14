using AutoMapper.Internal.Mappers;
using MediatR;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.DeleteRecursosTenant;

internal class DeleteRecursosTenantCommandHandler : ICommandHandler<DeleteRecursosTenantCommand, Guid>
{
    private readonly IRecursoTenantRepository _recursoRepository;
    private readonly IPartidaRecursoTenantRepository _partidaRecursoRepository;
    private readonly IPartidaTenantRepository _partidaRepository;

    public DeleteRecursosTenantCommandHandler(
        IPartidaRecursoTenantRepository partidaRecursoRepository,
        IRecursoTenantRepository recursoRepository,
        IPartidaTenantRepository partidaRepository
    )
    {
        _partidaRecursoRepository = partidaRecursoRepository;
        _recursoRepository = recursoRepository;
        _partidaRepository = partidaRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteRecursosTenantCommand request, CancellationToken cancellationToken)
    {
        var partidaRecursoExiste = await _partidaRecursoRepository.GetByIdAsync(new PartidaRecursoTenantId(Guid.Parse(request.Id)), cancellationToken);
        
         if(partidaRecursoExiste is null)
        {
            return Result.Failure<Guid>(PartidaRecursoTenantErrors.NotFound);
        }
        
        _partidaRecursoRepository.Delete(partidaRecursoExiste);

        await _partidaRecursoRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(Guid.Empty, Message.Create);

    }


}