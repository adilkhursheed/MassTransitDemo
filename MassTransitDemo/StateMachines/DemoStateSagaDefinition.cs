namespace Company.StateMachines
{
    using MassTransit;

    public class DemoStateSagaDefinition :SagaDefinition<DemoState>
    {
        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<DemoState> sagaConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}