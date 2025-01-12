
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.UpdateTituloTenant;


internal class UpdateTituloTenantCommandHandler : ICommandHandler<UpdateTituloTenantCommand, Guid>
{
    private readonly ITituloTenantRepository _tituloRepository;
       private readonly IPresupuestoEspecialidadTituloTenantRepository _presupuestoEspecialidadTituloRepository;

    public UpdateTituloTenantCommandHandler(
        ITituloTenantRepository tituloRepository, 
        IPresupuestoEspecialidadTituloTenantRepository presupuestoEspecialidadTituloRepository
    )
    {
        _tituloRepository = tituloRepository;
        _presupuestoEspecialidadTituloRepository = presupuestoEspecialidadTituloRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateTituloTenantCommand request, CancellationToken cancellationToken)
    {

        var presupuestoEspecialidadTitulo = await _presupuestoEspecialidadTituloRepository.GetByIdAsync(new PresupuestoEspecialidadTituloTenantId(Guid.Parse(request.Id)), cancellationToken);
        
        if(presupuestoEspecialidadTitulo is null)
        {
            return Result.Failure<Guid>(PresupuestoEspecialidadTituloTenantErrors.NotFound);

        }
        
        var titulo = await _tituloRepository.GetByIdAsync(presupuestoEspecialidadTitulo.TituloId!,cancellationToken);

        if(titulo is null)
        {
            return Result.Failure<Guid>(TituloTenantErrors.NotFound);
        }
        

        if(request.Nombre != titulo.Nombre)
        {
            var tituloExiste = await _tituloRepository.TituloExist(request.Nombre, presupuestoEspecialidadTitulo.EspecialidadId!, cancellationToken);
           
            if(tituloExiste)
            {
                return Result.Failure<Guid>(TituloTenantErrors.TituloInUse);
            }
        }

        titulo.Update(request.Nombre);

        _tituloRepository.Update(titulo);
        
        await _presupuestoEspecialidadTituloRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(Guid.Empty, Message.Update);

    }


}