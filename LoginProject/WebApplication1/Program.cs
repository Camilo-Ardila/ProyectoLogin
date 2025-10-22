using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Preload Aulas
SpaceModel.AddSpace("Aula101", "Aula", "Available", new DateTime(2025, 10, 22)); // Today
SpaceModel.AddSpace("Aula102", "Aula", "Not Available", new DateTime(2025, 10, 23)); // Tomorrow
SpaceModel.AddSpace("Aula103", "Aula", "Available", new DateTime(2025, 10, 24)); // Two days from now

// Preload Canchas
SpaceModel.AddSpace("Cancha1", "Cancha", "Available", new DateTime(2025, 10, 22)); // Today
SpaceModel.AddSpace("Cancha2", "Cancha", "Not Available", new DateTime(2025, 10, 25)); // Three days from now

// Preload Laboratorios
SpaceModel.AddSpace("Lab1", "Laboratorio", "Available", new DateTime(2025, 10, 23)); // Tomorrow
SpaceModel.AddSpace("Lab2", "Laboratorio", "Not Available", new DateTime(2025, 10, 26)); // Four days from now

Console.WriteLine("Preloaded spaces have been added to spaces.txt.");

UserModel.Users = UserModel.LoadUsers();
SpaceModel.Spaces = SpaceModel.LoadSpaces();

// Add controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Enable Swagger (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ CORS: allow Vue frontend on localhost:8080
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Middleware order
app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();

// ✅ Run on http://localhost:5000
app.Run("http://localhost:5000");
