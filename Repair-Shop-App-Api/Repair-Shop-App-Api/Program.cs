using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Data;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Repositories;
using Repair_Shop_App_Api.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Add services to the container
// -----------------------------

builder.Services.AddScoped<DeviceTypesRepository>();
builder.Services.AddScoped<DeviceTypesService>();
builder.Services.AddScoped<DevicesRepository>();
builder.Services.AddScoped<DevicesService>();
builder.Services.AddScoped<RepairsRepository>();
builder.Services.AddScoped<RepairsService>();
builder.Services.AddScoped<StatusStepsRepository>();
builder.Services.AddScoped<StatusStepsService>();

// EF Core - SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// CORS - allow Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => {
            policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Add controllers
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -----------------------------
// Configure middleware
// -----------------------------

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --------- ADD SEEDING CODE HERE ---------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // DeviceTypes seed
    if (!db.DeviceTypes.Any())
    {
        db.DeviceTypes.AddRange(
            new DeviceTypes { Name = "Mobile", IsActive = true },
            new DeviceTypes { Name = "Laptop", IsActive = true },
            new DeviceTypes { Name = "Tablet", IsActive = true },
            new DeviceTypes { Name = "Desktop", IsActive = true }
        );
        db.SaveChanges();
    }

    // StatusSteps seed
    if (!db.StatusSteps.Any())
    {
        db.StatusSteps.AddRange(
            new StatusSteps { Name = "Received", SortOrder = 1, IsActive = true },
            new StatusSteps { Name = "Diagnosed", SortOrder = 2, IsActive = true },
            new StatusSteps { Name = "Waiting for parts", SortOrder = 3, IsActive = true },
            new StatusSteps { Name = "In progress", SortOrder = 4, IsActive = true },
            new StatusSteps { Name = "Quality check", SortOrder = 5, IsActive = true },
            new StatusSteps { Name = "Need of pickup", SortOrder = 6, IsActive = true },
            new StatusSteps { Name = "Returned", SortOrder = 7, IsActive = true },
            new StatusSteps { Name = "Canceled", SortOrder = 8, IsActive = true }
        );
        db.SaveChanges();
    }
}
// ----------------------------------------


// Enable CORS
app.UseCors("AllowAngular");

app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the app
app.Run();