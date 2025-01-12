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

        var presupuestoEspecialidadTituloId = request.PresupuestoEspecialidadTituloId.Length > 0 ? new PresupuestoEspecialidadTituloTenantId(Guid.Parse(request.PresupuestoEspecialidadTituloId)) :new PresupuestoEspecialidadTituloTenantId(Guid.Empty);
        var partidaId = request.PartidaIdPadre.Length > 0 ? new PartidaTenantId(Guid.Parse(request.PartidaIdPadre)) :new PartidaTenantId(Guid.Empty);
        var partidaExiste = await _partidaRepository.PartidaExistsByName(request.Nombre,partidaId,presupuestoEspecialidadTituloId , cancellationToken);

        if(partidaExiste)
        {
            return Result.Failure<Guid>(PartidaTenantErrors.PartidaNameExists);
        }

        var tieneDependencia = request.PartidaIdPadre.Length > 0;
        
        var dependenciaPartida = new PartidaTenantId(tieneDependencia ?  Guid.Parse(request.PartidaIdPadre) : Guid.Empty);

        string? antiguoCorrelativo = "";

        if(request.Nivel == 0)
        {

            antiguoCorrelativo = await _partidaRepository.GetLastCorrelativoAsync(new PresupuestoEspecialidadTituloTenantId(Guid.Parse(request.PresupuestoEspecialidadTituloId)), cancellationToken);
        }
        else
        {
            antiguoCorrelativo = await _partidaRepository.GetLastCorrelativoDependenciaAsync(request.Nivel, new PartidaTenantId(Guid.Parse(request.PartidaIdPadre)), cancellationToken);
        }

        var correlativo = antiguoCorrelativo != null ?  int.Parse(antiguoCorrelativo!)+1 : 1;

        var correlativoFormateado = correlativo.ToString("D2");

        var newPartida = PartidaTenant.Create(tieneDependencia ? dependenciaPartida : null, request.Nombre,correlativoFormateado, request.Nivel);
        if(request.Nivel == 0)
        {

            var newEspecialidadTituloPartida = PresupuestoEspecialidadTituloPartidaTenant.Create(
                    new PresupuestoEspecialidadTituloTenantId(Guid.Parse(request.PresupuestoEspecialidadTituloId)),
                    newPartida.Id!
            );

             _especialidadTituloPartidaRepository.Add(newEspecialidadTituloPartida);
        }
        

        _partidaRepository.Add(newPartida);
        
        await _partidaRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(Guid.Empty, Message.Create);

    }


}