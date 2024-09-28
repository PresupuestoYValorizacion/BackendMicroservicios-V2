using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Domain.Root.PersonasNaturales;
using MsAcceso.Domain.Shared;


namespace MsAcceso.Application.Users.UpdatePersona;

internal class UpdatePersonaCommandHandler : ICommandHandler<UpdatePersonaCommand, Guid>
{
    private readonly IPersonaRepository _personaRepository;
    private readonly IPersonaNaturalRepository _personaNaturalRepository;
    private readonly IPersonaJuridicaRepository _personaJuridicaRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public UpdatePersonaCommandHandler(
        IUnitOfWorkTenant unitOfWork, 
        IPersonaRepository personaRepository, 
        IPersonaNaturalRepository personaNaturalRepository, 
        IPersonaJuridicaRepository personaJuridicaRepository)
    {
        _unitOfWork = unitOfWork;
        _personaRepository = personaRepository;
        _personaJuridicaRepository = personaJuridicaRepository;
        _personaNaturalRepository = personaNaturalRepository;
    }

    public async Task<Result<Guid>> Handle(UpdatePersonaCommand request, CancellationToken cancellationToken)
    {


        var persona = await _personaRepository.GetByIdAsync(request.Id, cancellationToken);

        if (persona is null)
        {
            return Result.Failure<Guid>(PersonaErrors.NotFound);
        }

        if (persona.TipoId != request.TipoId)
        {
            await HandleTipoPersonaChange(persona, request);
        }
        else
        {
            await HandleTipoPersonaUpdate(persona, request);
        }

        persona.Update(request.TipoId, request.TipoDocumentoId, request.NumeroDocumento);
        _personaRepository.Update(persona);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(persona.Id!.Value, Message.Update);
    }

    private async Task HandleTipoPersonaChange(Persona persona, UpdatePersonaCommand request)
    {
        switch (request.TipoId)
        {
            case ParametroId tipo when tipo == new ParametroId(TipoPersona.Natural):
                var personaNatural = PersonaNatural.Create(persona.Id!, request.NombreCompleto);
                _personaNaturalRepository.Add(personaNatural);
                _personaJuridicaRepository.DeleteById(persona.Id!);
                break;

            case ParametroId tipo when tipo == new ParametroId(TipoPersona.Juridico):
                var personaJuridica = PersonaJuridica.Create(persona.Id!, request.RazonSocial);
                _personaJuridicaRepository.Add(personaJuridica);
                _personaNaturalRepository.DeleteById(persona.Id!);
                break;
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task HandleTipoPersonaUpdate(Persona persona, UpdatePersonaCommand request)
    {
        switch (request.TipoId)
        {
            case ParametroId tipo when tipo == new ParametroId(TipoPersona.Natural):
                var personaNatural = await _personaNaturalRepository.GetByIdAsync(persona.Id!);
                personaNatural!.Update(request.NombreCompleto);
                _personaNaturalRepository.Update(personaNatural);
                break;

            case ParametroId tipo when tipo == new ParametroId(TipoPersona.Juridico):
                var personaJuridica = await _personaJuridicaRepository.GetByIdAsync(persona.Id!);
                personaJuridica!.Update(request.RazonSocial);
                _personaJuridicaRepository.Update(personaJuridica);
                break;
        }

        await _unitOfWork.SaveChangesAsync();
    }
}