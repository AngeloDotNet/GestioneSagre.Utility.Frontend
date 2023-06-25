namespace GestioneSagre.Utility.Frontend.BusinessLayer.MassTransit;

public static class ConfigureService
{
    public static void AddMassTransitService(this IServiceCollection services, IConfigurationSection rabbitConfig)
    {
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
    }
}