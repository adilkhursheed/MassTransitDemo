namespace MassTransit.Consumers
{
    using MassTransit;

    public class Step1Definition :
        ConsumerDefinition<Step1Consumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, 
            IConsumerConfigurator<Step1Consumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}