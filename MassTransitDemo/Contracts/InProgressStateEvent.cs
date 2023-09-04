namespace Contracts
{
    using System;

    public record InProgressStateEvent
    {
        public Guid CorrelationId { get; init; }
        public string Value { get; init; }
    }
}