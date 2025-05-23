WebApplication app = DefaultDistribtWebApplication.Create(webappBuilder =>
{
    webappBuilder.Services.AddReverseProxy()
        .LoadFromConfig(webappBuilder.Configuration.GetSection("ReverseProxy"));
});
app.MapGet("/", () => "Hello private gateway!");
app.MapReverseProxy();
DefaultDistribtWebApplication.Run(app);
