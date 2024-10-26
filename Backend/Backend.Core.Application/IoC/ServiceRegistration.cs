﻿using Backend.Core.Application.Interfaces.Services;
using Backend.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Backend.Core.Application.IoC
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            services.AddScoped<IIngredientService, IngredientService>();
            #endregion
        }
    }
}
