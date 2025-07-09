namespace WebhookTest.Model
{
    public sealed record WebhookSubscription(Guid Id, string EventType, string webhookUrl, DateTime CreatedOnUtc);

    public sealed record CreateWebhookRequest(string EventType, string WebhookUrl);
}
