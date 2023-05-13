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

        var rabbitConfig = Configuration.GetSection("RabbitMQ");

        services.AddSwaggerGenConfig($"{swaggerName}", "v1", string.Empty);
        services.AddMassTransit(x =>
        {
            x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.QueueExpiration = TimeSpan.FromSeconds(Convert.ToDouble(rabbitConfig["QueueExpiration"]));
                cfg.Host(rabbitConfig["Hostname"], rabbitConfig["VirtualHost"], h =>
                {
                    h.Username(rabbitConfig["Username"]);
                    h.Password(rabbitConfig["Password"]);
                });

                cfg.ConfigureEndpoints(context);
                cfg.ReceiveEndpoint(rabbitConfig["NameRequest-1"], e =>
                {
                    e.Durable = Convert.ToBoolean(rabbitConfig["Durable"]);
                    e.AutoDelete = Convert.ToBoolean(rabbitConfig["AutoDelete"]);
                    e.ExchangeType = rabbitConfig["ExchangeType"];
                    e.PrefetchCount = Convert.ToInt32(rabbitConfig["PrefetchCount"]);

                    e.UseMessageRetry(r => r.Interval(Convert.ToInt32(rabbitConfig["RetryCount"]),
                        Convert.ToInt32(rabbitConfig["RetryInterval"])
                    ));
                });
            }));

            x.AddRequestClient<ScontrinoPagatoListRequest>();
            x.AddRequestClient<ScontrinoStatoListRequest>();
        });

        services.AddMassTransit<ISecondBus>(x =>
        {
            x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.QueueExpiration = TimeSpan.FromSeconds(Convert.ToDouble(rabbitConfig["QueueExpiration"]));
                cfg.Host(rabbitConfig["Hostname"], rabbitConfig["VirtualHost"], h =>
                {
                    h.Username(rabbitConfig["Username"]);
                    h.Password(rabbitConfig["Password"]);
                });

                cfg.ConfigureEndpoints(context);
                cfg.ReceiveEndpoint(rabbitConfig["NameRequest-2"], e =>
                {
                    e.Durable = Convert.ToBoolean(rabbitConfig["Durable"]);
                    e.AutoDelete = Convert.ToBoolean(rabbitConfig["AutoDelete"]);
                    e.ExchangeType = rabbitConfig["ExchangeType"];
                    e.PrefetchCount = Convert.ToInt32(rabbitConfig["PrefetchCount"]);

                    e.UseMessageRetry(r => r.Interval(Convert.ToInt32(rabbitConfig["RetryCount"]),
                        Convert.ToInt32(rabbitConfig["RetryInterval"])
                    ));
                });
            }));

            x.AddRequestClient<TipoClienteListRequest>();
            x.AddRequestClient<TipoPagamentoListRequest>();
            x.AddRequestClient<TipoScontrinoListRequest>();
        });

        services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseCors($"{serviceName}");
        app.AddUseSwaggerUI($"{swaggerName} v1");

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}