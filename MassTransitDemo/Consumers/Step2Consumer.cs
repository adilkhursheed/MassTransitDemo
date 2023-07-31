
namespace MassTransit.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;
    using System;
    using System.Threading;

    public class Step2Consumer : IConsumer<Step2Context>
    {
        public Task Consume(ConsumeContext<Step2Context> context)
        {
            Console.WriteLine($"Step2Consumer: {context.Message.Value}");
            Thread.Sleep(TimeSpan.FromSeconds(3));

            return Task.CompletedTask;
        }
    }
}