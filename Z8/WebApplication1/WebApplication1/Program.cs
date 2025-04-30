using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddScoped<ITripsService, TripsService>(); // jeśli gdzieś wystąpi ITripsService, stwórz instancję TripsService
builder.Services.AddScoped<IClientsService, ClientsService>(); // jeśli gdzieś wystąpi ITripsService, stwórz instancję TripsService

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
app.Run();

