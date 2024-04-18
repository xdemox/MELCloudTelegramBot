﻿using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using MELCloudTelegramBot.Services;
using Microsoft.Extensions.DependencyInjection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register Bot configuration
        services.Configure<BotConfiguration>(
            context.Configuration.GetSection(BotConfiguration.Configuration));

        // Register named HttpClient to benefits from IHttpClientFactory
        // and consume it with ITelegramBotClient typed client.
        // More read:
        //  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients
        //  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
        services.AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    BotConfiguration? botConfig = new BotConfiguration() { BotToken = "yourTokenHere" };
                    TelegramBotClientOptions options = new(botConfig.BotToken);
                    return new TelegramBotClient(options, httpClient);
                });

        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();
    })
    .Build();

Console.WriteLine($@"
      .             *        .     .       .         .          .
           .     _     .     .            .       .         *  . 
    .    .   _  / |      .        .  *         _  .     .          .
            | \_| |         .                 | | __          .
          _ |     |   .   .           _       | |/  |
         | \      |           .      | |     /  |    \
         |  |     \                  | |    /   |     \
    ____/____\--...\______________...|__\-..|____\____/__
          .     .                       .       .   .
       .    . .     .   .        .          .              .
          .       .    .           .         .       .

Start listening {DateTime.Now}
=================================================================
", Console.ForegroundColor = ConsoleColor.Blue);

await host.RunAsync();

#pragma warning disable CA1050 // Declare types in namespaces
#pragma warning disable RCS1110 // Declare type inside namespace.
public class BotConfiguration
#pragma warning restore RCS1110 // Declare type inside namespace.
#pragma warning restore CA1050 // Declare types in namespaces
{
    public static readonly string Configuration = "BotConfiguration";

    public string BotToken { get; set; } = "";
}