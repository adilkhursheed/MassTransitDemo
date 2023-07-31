namespace MassTransit.Contracts
{
    public record Step2Context
    {
        public string Value { get; init; }
        public bool Status { get; set; }
    }
}