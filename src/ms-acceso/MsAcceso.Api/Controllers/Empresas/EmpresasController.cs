using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.Empresas;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/empresa")]
public class EmpresasController : ControllerBase
{

    private readonly ISender _sender;

    public EmpresasController(ISender sender)
    {
        _sender = sender;
    }

}