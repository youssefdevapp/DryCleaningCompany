using DryCleaningCompany.API;
using DryCleaningCompany.Application;
using DryCleaningCompany.Application.Services;
using DryCleaningCompany.Domain.Services;
using DryCleaningCompany.Infrastructure.Data;
using DryCleaningCompany.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure EF Core with an in-memory database for simplicity
builder.Services.AddDbContext<DryCleaningDbContext>(options =>
    options.UseInMemoryDatabase("ScheduleDb"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddFluentValidators();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();