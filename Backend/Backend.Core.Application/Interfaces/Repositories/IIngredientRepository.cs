using Backend.Core.Domain.Entities;

namespace Backend.Core.Application.Interfaces.Repositories
{
    public interface IIngredientRepository
    {
       Task<Ingredient?> AddIngredientAsync(Ingredient ingredient);
        Task<List<Ingredient>?> GetAllIngredientsAsync();
        Task<Ingredient?> GetIngredientById(int ingredientId);
        Task<Ingredient?> GetIngredientByName(string ingredientName);
        Task<Ingredient?> UpdateIngredient(int ingredientId, Ingredient ingredient);
        Task DeleteIngredientAsync(int ingredientId);
    }
}
