using AutoMapper;
using Backend.Core.Application.DTOs.Ingredient;
using Backend.Core.Domain.Entities;

namespace Backend.Core.Application.Mappers
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Ingredient
            CreateMap<IngredientDTO, Ingredient>()
                .ReverseMap();

            CreateMap<CreateIngredientDTO, Ingredient>()
                .ReverseMap();

            CreateMap<UpdateIngredientDTO, Ingredient>()
                .ReverseMap();
            #endregion
        }
    }
}
