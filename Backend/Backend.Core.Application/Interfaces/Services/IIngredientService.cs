using Backend.Core.Application.DTOs.Ingredient;
using Backend.Core.Application.Helpers;

namespace Backend.Core.Application.Interfaces.Services
{
    public interface IIngredientService
    {
        Task<IngredientDTO> AddIngredient(CreateIngredientDTO ingredientDTO);
        Task DeleteIngredient(int ingredientId);    
        Task<List<IngredientDTO>> GetAllIngredients(IngredientQueryObject query);
        Task<IngredientDTO> GetIngredientById(int ingredientId);
        Task<List<IngredientDTO>> GetIngredientsByName(string ingredientName);
        Task UpdateIngredient(int ingredientId, UpdateIngredientDTO ingredientDTO);
    }
}
