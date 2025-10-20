using Microsoft.EntityFrameworkCore;
using TaskList.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register EF DbContext and map ITaskContext to the concrete TaskContext
var connectionString = $"Data Source=tasklist.db";
builder.Services.AddDbContext<TaskContext>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<ITaskContext>(sp => sp.GetRequiredService<TaskContext>());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
