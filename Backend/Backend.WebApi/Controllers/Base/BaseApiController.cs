using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]s")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
