using Microsoft.EntityFrameworkCore;
using Whatsapp.Application.Interfaces;
using Whatsapp.Infrastructure;
using Whatsapp.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddHttpClient<IEvolutionApiService, EvolutionApiService>(client =>
{
    var uriString = builder.Configuration["Evolution:Uri"] ?? throw new InvalidOperationException("Configuration value 'Evolution:Uri' is missing.");
    client.BaseAddress = new Uri(uriString);
    Console.WriteLine($"uriString: {uriString}");
    client.DefaultRequestHeaders.Add(
        "apiKey",
        builder.Configuration["Evolution:ApiKey"]
    );
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Postgres")
    );
});
builder.Services.AddScoped<ContactRepository>();


builder.Services.AddOpenApi();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
