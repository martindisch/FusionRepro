using Microsoft.AspNetCore.Mvc;
using ZiggyCreatures.Caching.Fusion;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddLogging(c => c.AddSimpleConsole(o => o.TimestampFormat = "[HH:mm:ss] "))
    .AddFusionCache().WithDefaultEntryOptions(new FusionCacheEntryOptions
    {
        Duration = TimeSpan.FromSeconds(10),
        EagerRefreshThreshold = 0.1f,
    });
var app = builder.Build();

app.MapGet("/hello/{name}", async (
    string name,
    [FromServices] IFusionCache fusionCache,
    ILogger<Greeting> logger,
    CancellationToken cancellationToken) =>
{
    return await fusionCache.GetOrSetAsync(
        name,
        factoryToken => GetGreetingAsync(name, logger, factoryToken),
        token: cancellationToken);
});

app.Run();

static async Task<Greeting> GetGreetingAsync(string name, ILogger<Greeting> logger, CancellationToken cancellationToken)
{
    logger.LogInformation("Executing factory for {Key}", name);
    await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
    logger.LogInformation("Finishing factory for {Key}", name);

    return new Greeting($"Hello, {name}!");
}

public record Greeting(string Text);