using Backend.Core.Application.DTOs.Ingredient;
using Backend.Core.Application.Interfaces.Repositories;
using Backend.Core.Application.Interfaces.Services;

namespace Backend.Core.Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        public Task<CreateIngredientDTO> AddIngredient(CreateIngredientDTO ingredientDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteIngredient(int ingredientId)
        {
            throw new NotImplementedException();
        }

        public Task<List<IngredientDTO>> GetAllIngredients()
        {
            throw new NotImplementedException();
        }

        public Task<IngredientDTO> GetIngredientById(int ingredientId)
        {
            throw new NotImplementedException();
        }

        public Task<IngredientDTO> GetIngredientByName(string ingredientName)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateIngredientDTO?> UpdateIngredientById(int ingredientId, UpdateIngredientDTO ingredient)
        {
            throw new NotImplementedException();
        }
    }
}
