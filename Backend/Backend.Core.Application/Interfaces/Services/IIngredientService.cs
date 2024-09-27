using Backend.Core.Application.DTOs.Ingredient;

namespace Backend.Core.Application.Interfaces.Services
{
    public interface IIngredientService
    {
        Task<CreateIngredientDTO> AddIngredient(CreateIngredientDTO ingredientDTO);
        Task DeleteIngredient(int ingredientId);    
        Task<List<IngredientDTO>> GetAllIngredients();
        Task<IngredientDTO> GetIngredientById(int ingredientId);
        Task<IngredientDTO> GetIngredientByName(string ingredientName);
        Task<UpdateIngredientDTO?> UpdateIngredientById(int ingredientId, UpdateIngredientDTO ingredient);
    }
}
