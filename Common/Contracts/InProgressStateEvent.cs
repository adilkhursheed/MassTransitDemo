namespace Contracts
{
    using System;

    public interface InProgressStateEvent
    {
        public Guid CorrelationId { get; init; }
        public string Value { get; init; }
    }
}