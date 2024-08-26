using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Utils;
using MsAcceso.Application.Parametros.DesactiveParametros;
using MsAcceso.Application.Parametros.DeleteParametros;
using MsAcceso.Application.Parametros.GetByIdParametro;
using MsAcceso.Application.Parametros.GetParametroByPagination;
using MsAcceso.Application.Parametros.RegisterParametros;
using MsAcceso.Application.Parametros.UpdateParametros;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Application.Parametros.GetSubnivelesById;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Application.Roles.GetRolesByTipo;
using MsAcceso.Application.Licencias.GetAllLicencias;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Api.Controllers.Parametros;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/licencias")]
public class LicenciaController : Controller
{

    private readonly ISender _sender;

    public LicenciaController(ISender sender)
    {
        _sender = sender;
    }


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-all")]
    public async Task<ActionResult<List<LicenciaDto>>> GetAllLicencias()
    {
        var request = new GetAllLicenciasQuery {};
        var results = await _sender.Send(request);

        return Ok(results);
    }
}