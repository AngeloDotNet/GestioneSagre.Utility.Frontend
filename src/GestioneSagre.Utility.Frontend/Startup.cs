namespace GestioneSagre.Utility.Frontend;

public class Startup
{
    private readonly string serviceName = "GestioneSagre.Utility.Frontend";
    private readonly string swaggerName = "Gestione Sagre Utility Frontend";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy($"{serviceName}", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddSwaggerGenConfig($"{swaggerName}", "v1", string.Empty);
        services.AddSerilogSeqServices();

        var rabbitConfig = Configuration.GetSection("RabbitMQ");
        services.AddMassTransitService(rabbitConfig);

        services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseCors($"{serviceName}");
        app.AddUseSwaggerUI($"{swaggerName} v1");

        app.AddSerilogConfigureServices();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}