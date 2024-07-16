using Microsoft.EntityFrameworkCore;
using CalcApi.Models;
using CalcApi.Repositories;
using CalcApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<CalculationDb>(opt =>
    opt.UseInMemoryDatabase("Calculations"));
builder.Services.AddScoped<ICalculationRepository, CalculationRepository>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
