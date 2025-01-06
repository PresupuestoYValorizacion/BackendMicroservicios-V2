using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.PresupuestosTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.CreatePresupuestoTenant;

internal class CreatePresupuestoTenantCommandHandler : ICommandHandler<CreatePresupuestoTenantCommand, Guid>
{
    private readonly IPresupuestoTenantRepository _presupuestoRepository;
    private readonly IClienteTenantRepository _clienteRepository;
    private readonly ICarpetaPresupuestalTenantRepository _carpetaPresupuestalRepository;
    private readonly IProyectoTenantRepository _proyectoRepository;

    public CreatePresupuestoTenantCommandHandler(
        IPresupuestoTenantRepository presupuestoRepository,
        IClienteTenantRepository clienteRepository,
        ICarpetaPresupuestalTenantRepository carpetaPresupuestalRepository,
        IProyectoTenantRepository proyectoRepository
    )
    {
        _presupuestoRepository = presupuestoRepository;
        _clienteRepository = clienteRepository;
        _carpetaPresupuestalRepository = carpetaPresupuestalRepository;
        _proyectoRepository = proyectoRepository;
    }

    public async Task<Result<Guid>> Handle(CreatePresupuestoTenantCommand request, CancellationToken cancellationToken)
    {
        PresupuestoTenant? presupuesto = null;

        var proyecto = await _proyectoRepository.GetByIdAsync(new ProyectoTenantId(Guid.Parse(request.ProyectoId)), cancellationToken);

        if (proyecto is null)
        {
            return Result.Failure<Guid>(ProyectoTenantErrors.ProyectoNotExists);
        }

        bool isEdit = proyecto.PresupuestoId != null;


        if (isEdit)
        {
            presupuesto = await _presupuestoRepository.GetByIdAsync(proyecto.PresupuestoId!, cancellationToken);

            if (presupuesto is null)
            {
                return Result.Failure<Guid>(PresupuestoTenantErrors.PresupuestoNotExists);
            }
        }

        var cliente = await _clienteRepository.GetByIdAsync(new ClienteTenantId(Guid.Parse(request.ClienteId)), cancellationToken);

        if (cliente is null)
        {
            return Result.Failure<Guid>(ClienteTenantErrors.ClienteNotFound);
        }

        var carpetaPresupuestal = await _carpetaPresupuestalRepository.GetByIdAsync(new CarpetaPresupuestalTenantId(Guid.Parse(request.CarpetaPresupuestalId)), cancellationToken);

        if (carpetaPresupuestal is null)
        {
            return Result.Failure<Guid>(CarpetaPresupuestalTenantErrors.CarpetaPresupuestalNotFound);
        }

        if (isEdit && presupuesto != null)
        {
            presupuesto.Update(
                request.Codigo,
                request.Descripcion,
                new ClienteTenantId(Guid.Parse(request.ClienteId)),
                request.DepartamentoId,
                request.ProvinciaId,
                request.DistritoId,
                request.Fecha,
                request.Plazodias,
                request.JornadaDiariaId,
                request.MonedaId,
                request.PresupuestoBaseCD == 0 ? null : request.PresupuestoBaseCD,
                request.PresupuestoBaseCI == 0 ? null : request.PresupuestoBaseCI,
                request.TotalPresupuestoBase == 0 ? null : request.TotalPresupuestoBase,
                request.PresupuestoOfertaCD == 0 ? null : request.PresupuestoOfertaCD,
                request.PresupuestoOfertaCI == 0 ? null : request.PresupuestoOfertaCI,
                request.TotalPresupuestoOferta == 0 ? null : request.TotalPresupuestoOferta,
                new CarpetaPresupuestalTenantId(Guid.Parse(request.CarpetaPresupuestalId))
            );

            _presupuestoRepository.Update(presupuesto);
        }
        else
        {



            presupuesto = PresupuestoTenant.Create(
                request.Codigo,
                request.Descripcion,
                new ClienteTenantId(Guid.Parse(request.ClienteId)),
                request.DepartamentoId,
                request.ProvinciaId,
                request.DistritoId,
                request.Fecha,
                request.Plazodias,
                request.JornadaDiariaId,
                request.MonedaId,
                request.PresupuestoBaseCD == 0 ? null : request.PresupuestoBaseCD,
                request.PresupuestoBaseCI == 0 ? null : request.PresupuestoBaseCI,
                request.TotalPresupuestoBase == 0 ? null : request.TotalPresupuestoBase,
                request.PresupuestoOfertaCD == 0 ? null : request.PresupuestoOfertaCD,
                request.PresupuestoOfertaCI == 0 ? null : request.PresupuestoOfertaCI,
                request.TotalPresupuestoOferta == 0 ? null : request.TotalPresupuestoOferta,
                new CarpetaPresupuestalTenantId(Guid.Parse(request.CarpetaPresupuestalId))
            );

            _presupuestoRepository.Add(presupuesto);

            proyecto.UpdatePresupuesto(presupuesto.Id!);

            _proyectoRepository.Update(proyecto);
        }

        await _presupuestoRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(presupuesto.Id!.Value, isEdit ? Message.Update : Message.Create);

    }

}