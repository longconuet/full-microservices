using Common.Logging;
using Product.API.Extensions;
using Product.API.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .CreateBootstrapLogger();

Log.Information("Starting Product API up...");

try
{
    // Serilog
    //builder.Host.UseSerilog((context, configuration) => configuration
    //    .WriteTo.Console(
    //        outputTemplate: 
    //        "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    //    .Enrich.FromLogContext()
    //    .ReadFrom.Configuration(context.Configuration));

    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfigurations();

    // Add services to the container.
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    app.UseInfrastructure();

    app.MigrateDatabase<ProductDbContext>((context, _) =>
    {
        ProductContextSeed.SeedProductAsync(context, Log.Logger).Wait();
    }).Run();

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
    Log.Information("Shutdown Product API complete...");
    Log.CloseAndFlush();
}
