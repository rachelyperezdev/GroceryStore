using Asp.Versioning;
using Backend.Core.Application.DTOs.Ingredient;
using Backend.Core.Application.Helpers;
using Backend.Core.Application.Interfaces.Services;
using Backend.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IngredientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateIngredientDTO ingredientDTO)
        {
            try
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

                return CreatedAtAction(nameof(GetById), new { Id = ingredient.Id }, ingredient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] IngredientQueryObject query)
        {
            try
            {
                var ingredients = await _ingredientService.GetAllIngredients(query);

                if (ingredients.Any())
                {
                    return Ok(ingredients);
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var ingredient = await _ingredientService.GetIngredientById(id);

                if (ingredient is null)
                {
                    return NoContent();
                }

                return Ok(ingredient);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            try
            {
                var ingredient = await _ingredientService.GetIngredientsByName(name);

                if (ingredient is null)
                {
                    return NoContent();
                }

                return Ok(ingredient);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof (IngredientDTO))]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateIngredientDTO ingredientDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Internal error while updating the ingredient.");
                }

                var ingredientToUpdate = await _ingredientService.GetIngredientById(id);

                if (ingredientToUpdate is null)
                {
                    return NotFound("Ingredient not found.");
                }

                await _ingredientService.UpdateIngredient(id, ingredientDTO);

                return Ok(ingredientDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var ingredientToDelete = await _ingredientService.GetIngredientById(id);

                if (ingredientToDelete is null)
                {
                    return NotFound();
                }

                await _ingredientService.DeleteIngredient(id);

                return NoContent();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
