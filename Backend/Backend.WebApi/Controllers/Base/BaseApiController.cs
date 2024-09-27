using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
