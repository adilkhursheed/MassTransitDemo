namespace MassTransit.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;
    using System;
    using System.Threading;

    public class Step1Consumer : IConsumer<Step1Context>
    {
        private Random random { get; set; }
        public Step1Consumer()
        {
            random = new Random();
        }

        public Task Consume(ConsumeContext<Step1Context> context)
        {
            Console.WriteLine($"Step1Consumer: {context.Message.Value}");
            Thread.Sleep(TimeSpan.FromSeconds(3));

            if ((random.Next(111) % 2 == 0 ? true : false))
            {
                context.RespondAsync(new Step2Context() { Value = context.Message.Value, Status = (new Random(0).Next(1) % 2 == 0 ? true : false) });
            }
            return Task.CompletedTask;
        }
    }
}