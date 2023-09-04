using Company.StateMachines;
using Contracts;
using MassTransit;
using MassTransit.Contracts;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitDemo.WorkerService
{
    internal class StateMachineBackgroundService : BackgroundService
    {
        IBus bus;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IRequestClient<StatusCheckEvent> requestClient;

        public StateMachineBackgroundService(IBus bus, IPublishEndpoint publishEndpoint, IRequestClient<StatusCheckEvent> requestClient)
        {
            this.bus = bus;
            this.publishEndpoint = publishEndpoint;
            this.requestClient = requestClient;
        }
        private int count=0;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            {
                //await this.bus.Publish(new InProgressStateEvent()
                //{
                //    CorrelationId = Guid.NewGuid(),
                //    //CorrelationId = new Guid("8909e500-45e8-426e-8acd-646fe97190d6"),
                //    Value = $"Test Sate {count++}"

                //});
                var message=new { CorrelationId = Guid.NewGuid() };
                this.publishEndpoint.Publish<InitialStateEvent>(message);

                Task.Run(async () =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    var response= await this.requestClient.GetResponse<StatusCheckResponse>(new{ CorrelationId=message.CorrelationId });

                    if (response == null)
                    {

                    }
                });
            }
        }
    }
}
