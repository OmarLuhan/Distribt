WebApplication app = DefaultDistribtWebApplication.Create(webappBuilder =>
{
    webappBuilder.Services.AddReverseProxy()
        .LoadFromConfig(webappBuilder.Configuration.GetSection("ReverseProxy"));
});
app.MapReverseProxy();
app.MapGet("/", () => "Hello private gateway!");
DefaultDistribtWebApplication.Run(app);
