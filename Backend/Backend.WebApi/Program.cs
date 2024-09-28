using Backend.Infrastructure.Persistence.IoC;
using Backend.Core.Application.IoC;
using Backend.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtensions();
builder.Services.AddApiVersioning();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();    
app.UseAuthorization();
app.UseSwaggerExtensions();
app.UseHealthChecks("/health");
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
