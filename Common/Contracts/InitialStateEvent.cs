namespace Contracts
{
    using MassTransit;
    using System;

    public interface InitialStateEvent: CorrelatedBy<Guid>
    {
        public string Value { get; init; }
    }
}