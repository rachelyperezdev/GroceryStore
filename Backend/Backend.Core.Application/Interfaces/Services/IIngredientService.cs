using Backend.Core.Application.DTOs.Ingredient;
using Backend.Core.Application.Helpers;

namespace Backend.Core.Application.Interfaces.Services
{
    public interface IIngredientService
    {
        Task<CreateIngredientDTO> AddIngredient(CreateIngredientDTO ingredientDTO);
        Task DeleteIngredient(int ingredientId);    
        Task<List<IngredientDTO>> GetAllIngredients(IngredientQueryObject query);
        Task<IngredientDTO> GetIngredientById(int ingredientId);
        Task<IngredientDTO> GetIngredientByName(string ingredientName);
        Task UpdateIngredientById(int ingredientId, UpdateIngredientDTO ingredientDTO);
    }
}
