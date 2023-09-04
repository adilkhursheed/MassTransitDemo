namespace MassTransit
{
    public record StatusCheckEvent
    {
        public Guid CorrelationId { get; init; }
    }
    public record StatusCheckResponse
    {
        public int CurrentState { get; set; }
        public Guid CorrelationId { get; init; }
    }

}