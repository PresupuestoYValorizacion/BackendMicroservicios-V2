using AutoMapper.Internal.Mappers;
using MediatR;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.CreatePartidaTenant;



internal class CreatePartidaTenantCommandHandler : ICommandHandler<CreatePartidaTenantCommand, Guid>
{
    private readonly IPresupuestoEspecialidadTituloPartidaTenantRepository _especialidadTituloPartidaRepository;
    private readonly IPartidaTenantRepository _partidaRepository;

    public CreatePartidaTenantCommandHandler(
        IPresupuestoEspecialidadTituloPartidaTenantRepository especialidadTituloPartidaRepository,
        IPartidaTenantRepository partidaRepository
    )
    {
        _especialidadTituloPartidaRepository = especialidadTituloPartidaRepository;
        _partidaRepository = partidaRepository;
    }

    public async Task<Result<Guid>> Handle(CreatePartidaTenantCommand request, CancellationToken cancellationToken)
    {
        var partidaExiste = await _partidaRepository.PartidaExistsByName(request.Nombre, new PresupuestoEspecialidadTituloTenantId(Guid.Parse(request.PresupuestoEspecialidadTituloId)), cancellationToken);

        if(partidaExiste)
        {
            return Result.Failure<Guid>(PresupuestoEspecialidadTituloPartidaTenantErrors.PresupuestoExists);
        }

        var tieneDependencia = request.PartidaIdPadre.Length > 0;
        
        var dependenciaPartida = new PartidaTenantId(tieneDependencia ?  Guid.Parse(request.PartidaIdPadre) : Guid.Empty);


        var newPartida = PartidaTenant.Create(tieneDependencia ? dependenciaPartida : null, request.Nombre, request.Nivel);

        var antiguoCorrelativo = await _especialidadTituloPartidaRepository.GetLastCorrelativoAsync(new PresupuestoEspecialidadTituloTenantId(Guid.Parse(request.PresupuestoEspecialidadTituloId)), cancellationToken);

        var correlativo = antiguoCorrelativo != null ?  int.Parse(antiguoCorrelativo!)+1 : 1;

        var correlativoFormateado = correlativo.ToString("D2");

        if(request.Nivel == 0)
        {

            var newEspecialidadTituloPartida = PresupuestoEspecialidadTituloPartidaTenant.Create(
                    new PresupuestoEspecialidadTituloTenantId(Guid.Parse(request.PresupuestoEspecialidadTituloId)),
                    newPartida.Id!,
                    correlativoFormateado
            );

             _especialidadTituloPartidaRepository.Add(newEspecialidadTituloPartida);
        }
        

        _partidaRepository.Add(newPartida);

        await _especialidadTituloPartidaRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(Guid.Empty, Message.Create);

    }


}