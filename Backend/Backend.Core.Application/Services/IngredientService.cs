﻿using AutoMapper;
using Backend.Core.Application.DTOs.Ingredient;
using Backend.Core.Application.Helpers;
using Backend.Core.Application.Interfaces.Repositories;
using Backend.Core.Application.Interfaces.Services;
using Backend.Core.Domain.Entities;

namespace Backend.Core.Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<IngredientDTO> AddIngredient(CreateIngredientDTO ingredientDTO)
        {
            var ingredientModel = _mapper.Map<Ingredient>(ingredientDTO);
            ingredientModel = await _ingredientRepository.AddIngredientAsync(ingredientModel);

            var createdIngredientDTO = _mapper.Map<IngredientDTO>(ingredientModel);

            return createdIngredientDTO;
        }

        public async Task DeleteIngredient(int ingredientId)
        {
            var ingredientModel = await _ingredientRepository.GetIngredientByIdAsync(ingredientId);
            await _ingredientRepository.DeleteIngredientAsync(ingredientModel);
        }

        public async Task<List<IngredientDTO>> GetAllIngredients(IngredientQueryObject query)
        {
            var ingredientModels = await _ingredientRepository.GetAllIngredientsAsync(query);
            var ingredientsDTOs = _mapper.Map<List<IngredientDTO>>(ingredientModels);

            return ingredientsDTOs;
        }

        public async Task<IngredientDTO> GetIngredientById(int ingredientId)
        {
            var ingredientModel = await _ingredientRepository.GetIngredientByIdAsync(ingredientId);
            var ingredientDTO = _mapper.Map<IngredientDTO>(ingredientModel);

            return ingredientDTO;
        }

        public async Task<List<IngredientDTO>> GetIngredientsByName(string ingredientName)
        {
            var ingredientsModel = await _ingredientRepository.GetIngredientsByNameAsync(ingredientName);
            var ingredientDTO = _mapper.Map<List<IngredientDTO>>(ingredientsModel);

            return ingredientDTO;
        }

        public async Task UpdateIngredient(int ingredientId, UpdateIngredientDTO ingredientDTO)
        {
            var ingredientModel = _mapper.Map<Ingredient>(ingredientDTO);
            await _ingredientRepository.UpdateIngredientAsync(ingredientId, ingredientModel);
        }
    }
}
