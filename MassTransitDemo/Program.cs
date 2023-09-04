using System.Reflection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MassTransitDemo.WorkerService;

namespace MassTransit
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();

                        // By default, sagas are in-memory, but should be changed to a durable
                        // saga repository.
                        x.SetInMemorySagaRepositoryProvider();

                        var entryAssembly = Assembly.GetEntryAssembly();
                        //x.AddConsumer<Step1Consumer>();
                        x.AddConsumers(entryAssembly);
                        //x.AddRequestClient<StatusCheckEvent>();//(timeout: TimeSpan.FromSeconds(60));
                        x.AddSagaStateMachines(entryAssembly);
                        x.AddSagas(entryAssembly);
                        x.AddActivities(entryAssembly);

                        //If any messaging Queue is not available then InMemory can also be used
                        x.UsingInMemory((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });

                        //// Uncomment below if you have ActiveMQ up and running
                        //x.UsingRabbitMq((context, cfg) =>
                        //{
                        //    cfg.Host("localhost", "/", h =>
                        //    {
                        //        h.Username("guest");
                        //        h.Password("guest");
                        //    });

                        //    cfg.ConfigureEndpoints(context);
                        //});
                    });

                    //services.AddHostedService<ConsumersBackgroundService>();
                    services.AddHostedService<StateMachineBackgroundService>();
                });
    }
}
