using AutoMapper.Internal.Mappers;
using MediatR;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.CreateTituloTenant;



internal class CreateTituloTenantCommandHandler : ICommandHandler<CreateTituloTenantCommand, Guid>
{
    private readonly IPresupuestoEspecialidadTituloTenantRepository _especialidadTituloRepository;
    private readonly ITituloTenantRepository _tituloRepository;

    public CreateTituloTenantCommandHandler(
        IPresupuestoEspecialidadTituloTenantRepository especialidadTituloRepository,
        ITituloTenantRepository tituloRepository
    )
    {
        _especialidadTituloRepository = especialidadTituloRepository;
        _tituloRepository = tituloRepository;
    }

    public async Task<Result<Guid>> Handle(CreateTituloTenantCommand request, CancellationToken cancellationToken)
    {
        var especialidadExiste = await _tituloRepository.TituloExist(request.Nombre, new EspecialidadTenantId(Guid.Parse(request.EspecialidadId)), cancellationToken);

        if(especialidadExiste)
        {
            return Result.Failure<Guid>(PresupuestoEspecialidadTituloTenantErrors.PresupuestoExists);
        }
        var newTitulo = TituloTenant.Create(request.Nombre);

        var antiguoCorrelativo = await _especialidadTituloRepository.GetLastCorrelativoAsync(new EspecialidadTenantId(Guid.Parse(request.EspecialidadId)), cancellationToken);

        var correlativo = antiguoCorrelativo != null ?  int.Parse(antiguoCorrelativo!)+1 : 1;

        var correlativoFormateado = correlativo.ToString("D2");
        
        var newEspecialidadTitulo = PresupuestoEspecialidadTituloTenant.Create(
                new EspecialidadTenantId(Guid.Parse(request.EspecialidadId)),
                newTitulo.Id!,
                null,
                0,
                correlativoFormateado
        );

        _tituloRepository.Add(newTitulo);

        _especialidadTituloRepository.Add(newEspecialidadTitulo);

        await _especialidadTituloRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(Guid.Empty, Message.Create);

    }


}