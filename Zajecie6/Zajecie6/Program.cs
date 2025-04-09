// Builder append wzorzec programistyczny

//przekierowanie, autoryzacja robi się tu

// przygotowanie aplikacji

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Dodawanie kontrolerów do buildera
builder.Services.AddControllers();

var app = builder.Build();








// co sie dzieje dla kazdego zapytania http

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2. Mapowanie końcówek z kontrolerów
app.MapControllers();


app.UseHttpsRedirection();



app.Run();

