namespace WebhookTest.Model
{
    public sealed record Order(Guid Id, string CustomerName, decimal Amount, DateTime CreatedAt);

    public sealed record CreatedOrderRequest(string CustomerName, decimal Amount);
}
