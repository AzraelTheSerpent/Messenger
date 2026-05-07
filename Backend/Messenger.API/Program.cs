using Messenger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddOpenApi();
services.AddControllers();
services.AddSwaggerGen();
services.AddSignalR();
services.AddDbContext<MessengerDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Database"),
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null
            )
        )
    );

services.AddCors(options => 
{ 
    options.AddPolicy("DevelopmentCorsPolicy", corsPolicyBuilder => 
    { 
        corsPolicyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
    options.AddPolicy("ProductionCorsPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .WithOrigins("http://localhost:5000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevelopmentCorsPolicy");
}

if (app.Environment.IsProduction())
{
    app.UseCors("ProductionCorsPolicy");
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();