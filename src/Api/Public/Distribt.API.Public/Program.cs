using Distribt.Services.Subscriptions.Dtos;
using Distribt.Shared.Communication.Publisher.Integration;

WebApplication app = DefaultDistribtWebApplication.Create(webappBuilder =>
{
    webappBuilder.Services.AddReverseProxy()
        .LoadFromConfig(webappBuilder.Configuration.GetSection("ReverseProxy"));
        webappBuilder.Services.AddServiceBusIntegrationPublisher(webappBuilder.Configuration);
});
app.MapReverseProxy();
app.MapGet("/", () => "Hello public gateway!");
app.MapPost("/subscribe", async (SubscriptionDto subscriptionDto) =>
{
    var publisher = app.Services.GetRequiredService<IIntegrationMessagePublisher>();
    await publisher.Publish(subscriptionDto, routingKey: "subscription");
});
DefaultDistribtWebApplication.Run(app);
