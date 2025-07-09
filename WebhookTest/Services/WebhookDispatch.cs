using WebhookTest.Model;
using WebhookTest.Repositories;

namespace WebhookTest.Services
{
    internal sealed class WebhookDispatch(HttpClient httpClient, InMemoryWebhookSubscriptionRepository subscriptionRepository)
    {

        public async Task DispatchAsync(string eventType, object payload)
        {
            var subscriptions = subscriptionRepository.GetByEventType(eventType);

            foreach (WebhookSubscription subscription in subscriptions)
            {
                var request = new
                {
                    Id = Guid.NewGuid(),
                    subscription.EventType,
                    SubscriptionId = subscription.Id,
                    Timestamp = DateTime.UtcNow,
                    Data = payload
                };

                await httpClient.PostAsJsonAsync(subscription.webhookUrl, request); 
            }

        }
    }
}
