using Backend.Core.Domain.Entities;

namespace Backend.Core.Application.Interfaces.Repositories
{
    public interface IIngredientRepository
    {
       Task<Ingredient?> AddIngredientAsync(Ingredient ingredient);
        Task<List<Ingredient>?> GetAllIngredientsAsync();
        Task<Ingredient?> GetIngredientByIdAsync(int ingredientId);
        Task<Ingredient?> GetIngredientByNameAsync(string ingredientName);
        Task<Ingredient?> UpdateIngredientAsync(int ingredientId, Ingredient ingredient);
        Task DeleteIngredientAsync(Ingredient ingredient);
    }
}
