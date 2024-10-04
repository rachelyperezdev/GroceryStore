using Asp.Versioning;
using Backend.Core.Application.DTOs.Ingredient;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateIngredientDTO ingredientDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var ingredient = await _ingredientService.AddIngredient(ingredientDTO);

            if (ingredient is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetByName), new { Name = ingredientDTO.Name }, ingredient);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] IngredientQueryObject query)
        {
            var ingredients = await _ingredientService.GetAllIngredients(query);

            if (ingredients.Any())
            {
                return Ok(ingredients);
            }

            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var ingredient = await _ingredientService.GetIngredientById(id);

            if (ingredient is null)
            {
                return NoContent();
            }

            return Ok(ingredient);
        }

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var ingredient = await _ingredientService.GetIngredientsByName(name);

            if(ingredient is null)
            {
                return NoContent();
            }

            return Ok(ingredient);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateIngredientDTO ingredientDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Internal error while updating the ingredient.");
            }

            var ingredientToUpdate = await _ingredientService.GetIngredientById(id);

            if(ingredientToUpdate is null)
            {
                return NotFound("Ingredient not found.");
            }

            await _ingredientService.UpdateIngredient(id, ingredientDTO);

            return Ok(ingredientDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var ingredientToDelete = await _ingredientService.GetIngredientById(id);

            if(ingredientToDelete is null)
            {
                return NotFound();
            }

            await _ingredientService.DeleteIngredient(id);

            return NoContent();
        }
    }
}
