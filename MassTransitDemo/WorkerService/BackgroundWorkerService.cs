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
    internal class BackgroundWorkerService : BackgroundService
    {
        IBus bus;
        public BackgroundWorkerService(IBus bus)
        {
            this.bus = bus;
        }
        private int count=0;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await this.bus.Publish(new Step1Context()
                {
                    Value = $"Test Message {count++}"
                });
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }            

        }
    }
}
