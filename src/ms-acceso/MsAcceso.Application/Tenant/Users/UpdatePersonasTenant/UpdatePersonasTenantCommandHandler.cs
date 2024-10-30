using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;


namespace MsAcceso.Application.Root.Users.UpdatePersona;

internal class UpdatePersonasTenantCommandHandler : ICommandHandler<UpdatePersonasTenantCommand, Guid>
{
    private readonly IPersonaTenantRepository _personaTenantRepository;
    private readonly IPersonaNaturalTenantRepository _personaNaturalTenantRepository;
    private readonly IPersonaJuridicaTenantRepository _personaJuridicaTenantRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public UpdatePersonasTenantCommandHandler(
        IUnitOfWorkApplication unitOfWork, 
        IPersonaTenantRepository personaTenantRepository, 
        IPersonaNaturalTenantRepository personaNaturalTenantRepository, 
        IPersonaJuridicaTenantRepository personaJuridicaTenantRepository)
    {
        _unitOfWork = unitOfWork;
        _personaTenantRepository = personaTenantRepository;
        _personaJuridicaTenantRepository = personaJuridicaTenantRepository;
        _personaNaturalTenantRepository = personaNaturalTenantRepository;
    }

    public async Task<Result<Guid>> Handle(UpdatePersonasTenantCommand request, CancellationToken cancellationToken)
    {


        var personaTenant = await _personaTenantRepository.GetByIdAsync(request.Id, cancellationToken);

        if (personaTenant is null)
        {
            return Result.Failure<Guid>(PersonaTenantErrors.NotFound);
        }

        if (personaTenant.TipoId != request.TipoId)
        {
            await HandleTipoPersonaChange(personaTenant, request);
        }
        else
        {
            await HandleTipoPersonaUpdate(personaTenant, request);
        }

        personaTenant.Update(request.TipoId, request.TipoDocumentoId, request.NumeroDocumento);
        _personaTenantRepository.Update(personaTenant);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(personaTenant.Id!.Value, Message.Update);
    }

    private async Task HandleTipoPersonaChange(PersonaTenant personaTenant, UpdatePersonasTenantCommand request)
    {
        switch (request.TipoId)
        {
            case int tipo when tipo == TipoPersona.Natural:
                var personaNaturalTenant = PersonaNaturalTenant.Create(personaTenant.Id!, request.NombreCompleto);
                _personaNaturalTenantRepository.Add(personaNaturalTenant);
                _personaJuridicaTenantRepository.DeleteById(personaTenant.Id!);
                break;

            case int tipo when tipo == TipoPersona.Juridico:
                var personaJuridicapersonaNaturalTenant = PersonaJuridicaTenant.Create(personaTenant.Id!, request.RazonSocial);
                _personaJuridicaTenantRepository.Add(personaJuridicapersonaNaturalTenant);
                _personaNaturalTenantRepository.DeleteById(personaTenant.Id!);
                break;
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task HandleTipoPersonaUpdate(PersonaTenant persona, UpdatePersonasTenantCommand request)
    {
        switch (request.TipoId)
        {
            case int tipo when tipo == TipoPersona.Natural:
                var personaNaturalTenant = await _personaNaturalTenantRepository.GetByIdAsync(persona.Id!);
                personaNaturalTenant!.Update(request.NombreCompleto);
                _personaNaturalTenantRepository.Update(personaNaturalTenant);
                break;

            case int tipo when tipo == TipoPersona.Juridico:
                var personaJuridicaTenant = await _personaJuridicaTenantRepository.GetByIdAsync(persona.Id!);
                personaJuridicaTenant!.Update(request.RazonSocial);
                _personaJuridicaTenantRepository.Update(personaJuridicaTenant);
                break;
        }

        await _unitOfWork.SaveChangesAsync();
    }
}