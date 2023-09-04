namespace Company.StateMachines
{
    using Contracts;
    using MassTransit;
    using MassTransit.Transports;

    public class DemoStateMachine :
        MassTransitStateMachine<DemoState>
    {

        static Guid testGuide;
        public DemoStateMachine()
        {
            InstanceState(x => x.CurrentState, Initiated);

            Event(() => InitialStateEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => StatusCheckEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Initially(
                When(InitialStateEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"InitialThen:\t\t{context.Instance.CorrelationId},{context.Instance.CurrentState},{context.Instance.Value}, {context.Data.Value}");
                        context.Instance.Value = context.Data.Value;
                        testGuide = context.Data.CorrelationId;
                        context.Publish<InProgressStateEvent>(new
                        {
                            CorrelationId = context.Data.CorrelationId, /// it should trigger inprogress
                            //CorrelationId = Guid.NewGuid(),/// it should not trigger inprogress, because the new guide is not passed through initial
                            Value = $"{context.Data.CorrelationId}-Inprog"
                        });
                    })
                    .TransitionTo(Initiated)
            //.Publish( c => new InProgressStateEvent(){ 
            //    CorrelationId = (Guid)c.CorrelationId
            //})
            );
            During(Initiated,
                When(InProgressStateEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"InProgress:\t\t{context.Instance.CorrelationId},{context.Instance.CurrentState},{context.Instance.Value}, {context.Data.Value}");
                        context.Instance.Value = context.Data.Value;
                        Thread.Sleep(TimeSpan.FromSeconds(20));
                        context.Publish<CompletedStateEvent>(new
                        {
                            CorrelationId = context.Data.CorrelationId, /// it should trigger completed
                            //CorrelationId = Guid.NewGuid(),/// it should not trigger completed, because the new guide is not passed through inprogress
                            Value = $"{context.Data.CorrelationId}-complete"
                        });
                    })
                    .TransitionTo(InProgress)
                //.Publish(new CompletedStateEvent()
                //{
                //    CorrelationId = new Guid("8909e500-45e8-426e-8acd-646fe97190d6"), // if this guide is the initial guide then it will trigger completedEvent
                //    Value = $"Publish Completed new"
                //})
                );

            During(InProgress,
            When(CompletedStateEvent)
                .Then(context =>
                {
                    Console.WriteLine($"Completed:\t\t{context.Instance.CorrelationId},{context.Instance.CurrentState},{context.Instance.Value}, {context.Data.Value}");
                    context.Instance.Value = context.Data.Value;
                })
                .TransitionTo(Completed));
            DuringAny(
                When(StatusCheckEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"StatusCheck:\t\t{context.Instance.CorrelationId},{context.Instance.CurrentState},{context.Instance.Value}");
                        //context.Instance.Value = context.Data.Value;
                        context.Respond(new StatusCheckResponse()
                        {
                            CorrelationId = context.Data.CorrelationId,
                            CurrentState = context.Instance.CurrentState
                        });
                    }));

            SetCompletedWhenFinalized();
        }

        public MassTransit.State Initiated { get; private set; }

        public Event<InitialStateEvent> InitialStateEvent { get; private set; }


        public MassTransit.State InProgress { get; private set; }
        public Event<InProgressStateEvent> InProgressStateEvent { get; private set; }


        public MassTransit.State Completed { get; private set; }
        public Event<CompletedStateEvent> CompletedStateEvent { get; private set; }
        //public MassTransit.State StatusCheck { get; private set; }
        public Event<StatusCheckEvent> StatusCheckEvent { get; private set; }
    }
}