
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.UpdatePartidaTenant;



internal class UpdatePartidaTenantCommandHandler : ICommandHandler<UpdatePartidaTenantCommand, Guid>
{
    private readonly IPartidaTenantRepository _partidaRepository;

    public UpdatePartidaTenantCommandHandler(
        IPartidaTenantRepository partidaRepository
    )
    {
        _partidaRepository = partidaRepository;
    }

    public async Task<Result<Guid>> Handle(UpdatePartidaTenantCommand request, CancellationToken cancellationToken)
    {

        var partida = await _partidaRepository.GetByIdWithIncludesAsync(new PartidaTenantId(Guid.Parse(request.Id)),cancellationToken);

        if(partida is null)
        {
            return Result.Failure<Guid>(PartidaTenantErrors.PartidaNotFound);
        }

        if(request.Nombre != partida.Nombre)
        {
            var presupuestoEspecialidadTituloId = new PresupuestoEspecialidadTituloTenantId(Guid.Empty);
            
            if(partida.PresupuestosEspecialidadesTitulosPartidas?.Count >0)
            {
                presupuestoEspecialidadTituloId = partida!.PresupuestosEspecialidadesTitulosPartidas[0].PresupuestoEspecialidadTituloId;

            }
            var partidaId = partida!.Dependencia != null ? partida.Dependencia :new PartidaTenantId(Guid.Empty);
            var partidaExiste = await _partidaRepository.PartidaExistsByName(request.Nombre,partidaId,presupuestoEspecialidadTituloId! , cancellationToken);

            if(partidaExiste)
            {
                return Result.Failure<Guid>(PartidaTenantErrors.PartidaNameExists);
            }
        }

        partida.Update(request.Nombre);

        _partidaRepository.Update(partida);
        
        await _partidaRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(Guid.Empty, Message.Update);

    }


}