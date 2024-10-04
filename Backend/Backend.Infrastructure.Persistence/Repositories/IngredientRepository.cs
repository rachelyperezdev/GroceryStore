using Backend.Core.Application.Helpers;
using Backend.Core.Application.Interfaces.Repositories;
using Backend.Core.Domain.Entities;
using Backend.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<List<Ingredient>?> GetAllIngredientsAsync(IngredientQueryObject query)
        {
            var ingredients = _context.Set<Ingredient>()
                                      .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                ingredients = ingredients.Where(i => i.Name.Contains(query.Name));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.IsDescendant)
                {
                    ingredients = ingredients.OrderByDescending(GetSortProperty(query));
                }
                else
                {
                    ingredients = ingredients.OrderBy(GetSortProperty(query));
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await ingredients
                            .Skip(skipNumber)  
                            .Take(query.PageSize)
                            .ToListAsync();
        }

        public async Task<Ingredient?> GetIngredientByIdAsync(int ingredientId)
        {
            var ingredient = await _context.Set<Ingredient>().FindAsync(ingredientId);

            if(ingredient is null)
            {
                return null;
            }

            return ingredient;
        }

        public async Task<List<Ingredient?>> GetIngredientsByNameAsync(string ingredientName)
        {
            var ingredient = await _context.Set<Ingredient>()
                                      .Where(ingredient => 
                                             ingredient.Name.Contains(ingredientName))
                                      .ToListAsync ();

            return ingredient;
        }

        public async Task<Ingredient?> UpdateIngredientAsync(int ingredientId, Ingredient ingredient)
        {
            var entry = await _context.Set<Ingredient>().FindAsync(ingredientId);

            if (entry is null)
            {
                return null;
            }

            ingredient.Id = ingredientId;
            ingredient.CreatedBy = entry.CreatedBy;
            ingredient.CreatedAt = entry.CreatedAt;

            _context.Entry(entry).CurrentValues.SetValues(ingredient);
            await _context.SaveChangesAsync();

            return ingredient;
        }

        #region Private Methods
        private Expression<Func<Ingredient, object>> GetSortProperty(IngredientQueryObject query)
        {
            Expression<Func<Ingredient, object>> keySelector = query.SortBy?.ToLower() switch
            {
                "name" => ingredient => ingredient.Name,
                "price" => ingredient => ingredient.Price,
                _ => ingredient => ingredient.Name
            };

            return keySelector;
        }
        #endregion
    }
}
