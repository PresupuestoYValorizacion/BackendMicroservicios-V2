using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.DeleteTituloPartidaTenant;

internal sealed class DeleteTituloPartidaTenantCommandHandler : ICommandHandler<DeleteTituloPartidaTenantCommand, Guid>
{
    private readonly ITituloTenantRepository _tituloRepository;
    private readonly IPresupuestoEspecialidadTituloTenantRepository _presupuestoEspecialidadTituloRepository;
    private readonly IPresupuestoEspecialidadTituloPartidaTenantRepository _presupuestoEspecialidadTituloPartidaRepository;
    private readonly IPartidaTenantRepository _partidaRepository;

    public DeleteTituloPartidaTenantCommandHandler(
        ITituloTenantRepository tituloRepository,
        IPresupuestoEspecialidadTituloTenantRepository presupuestoEspecialidadTituloRepository,
        IPresupuestoEspecialidadTituloPartidaTenantRepository presupuestoEspecialidadTituloPartidaRepository,
        IPartidaTenantRepository partidaRepository
    )
    {
        _tituloRepository = tituloRepository;
        _partidaRepository = partidaRepository;
        _presupuestoEspecialidadTituloRepository = presupuestoEspecialidadTituloRepository;
        _presupuestoEspecialidadTituloPartidaRepository = presupuestoEspecialidadTituloPartidaRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteTituloPartidaTenantCommand request, CancellationToken cancellationToken)
    {


        if (request.IsTitulo)
        {
            return await DeleteTitulo(request, cancellationToken);
        }
        else
        {

            return await DeletePartida(request, cancellationToken);
        }

    }

    private async Task<Result<Guid>> DeleteTitulo(DeleteTituloPartidaTenantCommand request, CancellationToken cancellationToken)
    {

        try
        {
            var presupuestoEspecialidadTitulo = await _presupuestoEspecialidadTituloRepository.GetByIdAsync(new PresupuestoEspecialidadTituloTenantId(Guid.Parse(request.Id)), cancellationToken);

            if (presupuestoEspecialidadTitulo is null)
            {
                return Result.Failure<Guid>(PresupuestoEspecialidadTituloTenantErrors.NotFound);
            }
            var titulo = await _tituloRepository.GetByIdAsync(presupuestoEspecialidadTitulo.TituloId!, cancellationToken);

            if (titulo is null)
            {
                return Result.Failure<Guid>(TituloTenantErrors.NotFound);
            }

            _tituloRepository.Delete(titulo);

            _presupuestoEspecialidadTituloRepository.Delete(presupuestoEspecialidadTitulo);

            await _presupuestoEspecialidadTituloRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(Guid.Empty, Message.Delete);

        }
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<Guid>(PresupuestoEspecialidadTituloPartidaTenantErrors.PresupuestoInUse);

        }
    }

    private async Task<Result<Guid>> DeletePartida(DeleteTituloPartidaTenantCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var partida = await _partidaRepository.GetByIdWithIncludesAsync(new PartidaTenantId(Guid.Parse(request.Id)), cancellationToken);

            if (partida is null)
            {
                return Result.Failure<Guid>(PartidaTenantErrors.PartidaNotFound);
            }

            if (partida.PresupuestosEspecialidadesTitulosPartidas.Count > 0)
            {

                var presupuestoEspecialidadTituloPartida = await _presupuestoEspecialidadTituloPartidaRepository.GetByIdAsync(partida.PresupuestosEspecialidadesTitulosPartidas[0].Id!, cancellationToken);

                _presupuestoEspecialidadTituloPartidaRepository.Delete(presupuestoEspecialidadTituloPartida!);

            }


            _partidaRepository.Delete(partida);

            await _presupuestoEspecialidadTituloRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(Guid.Empty, Message.Delete);

        }
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<Guid>(PartidaTenantErrors.PartidaInUse);

        }
    }
}