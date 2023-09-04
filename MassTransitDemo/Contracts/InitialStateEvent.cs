namespace Contracts
{
    using MassTransit;
    using System;

    public record InitialStateEvent
    {
        public Guid CorrelationId { get; init; }
        public string Value { get; init; }
    }
    //public interface InitialStateEvent : CorrelatedBy<Guid>
    //{
    //    public string Value { get; init; }
    //}
}