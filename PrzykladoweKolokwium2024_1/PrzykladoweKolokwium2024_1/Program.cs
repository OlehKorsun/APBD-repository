using Microsoft.AspNetCore.Diagnostics;
using PrzykladoweKolokwium2024_1.Middlewares;
using PrzykladoweKolokwium2024_1.Repositories;
using PrzykladoweKolokwium2024_1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers(); ////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


// rejestrowanie zależności
builder.Services.AddScoped<IItemsService, ItemsService>(); //// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
builder.Services.AddScoped<IItemsRepository, ItemsRepository>();

builder.Services.AddSwaggerGen();   //////// !!!!!!!!!!!!!!!!!!!!!!!

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseGlobalExceptionHandling();   ///// do Middlewares !!!!!!!!!!!!!!!!!!!!!


app.UseSwaggerUI();    ///////   !!!!!!!!!!!!!!!!!!!!!!

app.UseHttpsRedirection();


app.MapControllers();    ////// !!!!!!!!!!!!!!!!!!!!!!!

app.Run();


// Project (ikonka C#) -> tools -> .NET user secrets -> {"ConnectionString" : "tutaj umieścić connection String"}