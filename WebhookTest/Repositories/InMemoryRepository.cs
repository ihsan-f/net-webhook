using WebhookTest.Model;

namespace WebhookTest.Repositories
{
    internal sealed class InMemoryWebhookSubscriptionRepository
    {
        private readonly List<WebhookSubscription> _subscription = [];
        
        public void Add(WebhookSubscription subscription)
        {
            _subscription.Add(subscription);
        }

        public IReadOnlyList<WebhookSubscription> GetByEventType(string eventType)
        {
            return _subscription.Where(s => s.EventType == eventType).ToList().AsReadOnly();
        }
    }

    internal sealed class InMemoryRepository
    {
        private readonly List<Order> _order = [];

        public void Add(Order order)
        {
            _order.Add(order);
        }

        public IReadOnlyList<Order> GetAll()
        {
            return _order.AsReadOnly();
        }
    }
}
