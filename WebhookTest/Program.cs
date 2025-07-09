using WebhookTest.Model;
using WebhookTest.Repositories;
using WebhookTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<InMemoryRepository>();
builder.Services.AddSingleton<InMemoryWebhookSubscriptionRepository>();
builder.Services.AddHttpClient<WebhookDispatch>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/wh/subscriptions", (
    CreateWebhookRequest request,
    InMemoryWebhookSubscriptionRepository subscriptionRepository) =>
{
    var subscription = new WebhookSubscription(
        Guid.NewGuid(),
        request.EventType,
        request.WebhookUrl,
        DateTime.UtcNow);

    subscriptionRepository.Add(subscription);
    return Results.Ok(subscription);
});


app.MapPost("/orders", async (
    CreatedOrderRequest request,
    InMemoryRepository repository,
    WebhookDispatch webhookDispatcher) =>
{
    var order = new Order(
        Guid.NewGuid(),
        request.CustomerName,
        request.Amount,
        DateTime.UtcNow);

    repository.Add(order);

    await webhookDispatcher.DispatchAsync("order.created", order);
    return Results.Ok(order);
});

app.MapGet("/orders", (InMemoryRepository repository) =>
{
    return Results.Ok(repository.GetAll());
});

app.Run();
