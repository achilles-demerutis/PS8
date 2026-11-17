using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("WebRestOracleConnection");

builder.Services.AddDbContext<WebRestOracleContext>(options =>
    options.UseOracle(connectionString)
        .UseLoggerFactory(
            LoggerFactory.Create(logging => logging.AddConsole())
        )
);

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