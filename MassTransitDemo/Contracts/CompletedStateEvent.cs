namespace Contracts
{
    using System;

    public record CompletedStateEvent
    {
        public Guid CorrelationId { get; init; }
        public string Value { get; init; }
    }
}