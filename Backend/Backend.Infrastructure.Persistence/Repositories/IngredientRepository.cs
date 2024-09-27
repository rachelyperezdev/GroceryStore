using Backend.Core.Application.Interfaces.Repositories;
using Backend.Core.Domain.Entities;
using Backend.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationContext _context;
        public IngredientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Ingredient?> AddIngredientAsync(Ingredient ingredient)
        {
            await _context.Set<Ingredient>().AddAsync(ingredient);
            await _context.SaveChangesAsync();

            return ingredient;
        }

        public async Task DeleteIngredientAsync(Ingredient ingredient)
        {
            _context.Set<Ingredient>().Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ingredient>?> GetAllIngredientsAsync()
        {
            return await _context.Set<Ingredient>().ToListAsync();
        }

        public async Task<Ingredient?> GetIngredientById(int ingredientId)
        {
            var ingredient = await _context.Set<Ingredient>().FindAsync(ingredientId);

            if(ingredient is null)
            {
                return null;
            }

            return ingredient;
        }

        public async Task<Ingredient?> GetIngredientByName(string ingredientName)
        {
            var ingredient = await _context.Set<Ingredient>()
                                           .FirstOrDefaultAsync(i =>  i.Name == ingredientName);

            return ingredient;
        }

        public async Task<Ingredient?> UpdateIngredient(int ingredientId, Ingredient ingredient)
        {
            var entry = await _context.Set<Ingredient>().FindAsync(ingredientId);

            if (entry is null)
            {
                return null;
            }

            _context.Entry(entry).CurrentValues.SetValues(ingredient);
            await _context.SaveChangesAsync();

            return ingredient;
        }
    }
}
