using Asp.Versioning;
using Backend.Core.Application.Helpers;
using Backend.Core.Application.Interfaces.Services;
using Backend.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class IngredientController : BaseApiController
    {
        private IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] IngredientQueryObject query)
        {

        }
    }
}
