using Serilog;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;
using Common.Logging;
using Ordering.Application;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Starting Ordering API up...");

try
{
    // Add services to the container.
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationServices();

    builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Init and seed db
    using (var scope = app.Services.CreateScope())
    {
        var orderDbContextSeed = scope.ServiceProvider.GetRequiredService<OrderDbContextSeed>();
        await orderDbContextSeed.InitialiseAsync();
        await orderDbContextSeed.TrySeedAsync();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shutdown Ordering API complete...");
    Log.CloseAndFlush();
}
