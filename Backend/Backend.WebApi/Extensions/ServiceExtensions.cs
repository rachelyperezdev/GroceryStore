using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace Backend.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtensions(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, 
                                                           "*.xml", 
                                                           searchOption: SearchOption.AllDirectories
                                                           ).ToList();

                xmlFiles.ForEach(xmlFile =>
                {
                    options.IncludeXmlComments(xmlFile);
                });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "GroceryStoreApp API",
                    Description = "This API will be responsible of distributing data",
                    Contact = new OpenApiContact()
                    {
                        Name = "Rachely Pérez",
                        Email = "rachely.perez.31@gmail.com",
                        Url = new Uri("https://github.com/rachelyperezdev")
                    }
                });

                options.DescribeAllParametersInCamelCase();
            });

        }

        public static void AddApiVersioningExtensions(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
        }
    }
}
