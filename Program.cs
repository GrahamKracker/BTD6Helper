using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static BTD6Helper.Credentials;

namespace BTD6Helper;

public static class Program
{
    private static readonly HttpClient HttpClient = new();
    
    private static readonly DiscordSocketConfig SocketConfig = new()
    {
        AlwaysDownloadUsers = false,
        GatewayIntents = GatewayIntents.GuildMessages | GatewayIntents.Guilds | GatewayIntents.MessageContent,
        LargeThreshold = 0,
        MessageCacheSize = 0,
        AlwaysDownloadDefaultStickers = false,
        AlwaysResolveStickers = false
    };

    private static readonly DiscordSocketClient Client = new(SocketConfig);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Main()
    {
        Client.Log += message =>
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"[General/{message.Severity}] {message.ToString()}");
            });
        };
        Client.MessageReceived += MessageReceivedAsync;
        
        MainTask().GetAwaiter().GetResult();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task MainTask()
    {
        _ = Client.LoginAsync(TokenType.Bot, Token).ContinueWith(async _ =>
        {
            await Client.StartAsync().ConfigureAwait(false);
        });

        // Block the program until it is closed.
        await Task.Delay(Timeout.Infinite).ConfigureAwait(false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Task MessageReceivedAsync(SocketMessage message)
    {
        // The bot should never respond to itself or another bot.
        if (message.Author.IsBot)
            return Task.CompletedTask;
        
        switch (message.Content)
        {
            case "!log":
                _ = message.Channel.SendMessageAsync("How to find your Log file:\n\n- go to your game's root folder. It's the folder that contains your Mods folder\n- open the MelonLoader folder\n- find the file called Latest or Latest.log\n- drag and drop that file here");
                return Task.CompletedTask;
            case "!uninstall":
                _ = message.Channel.SendMessageAsync("**How to uninstall Mods and MelonLoader:**\nRemove the MelonLoader, Mods, Plugins, UserData, UserLibs folders as well as dobby.dll, notice.txt and version.dll.");
                return Task.CompletedTask;
            case ".":
                return Task.CompletedTask;
        }
        
        if (message.Attachments.Count == 0)
            return Task.CompletedTask;
        
        HttpClient.GetByteArrayAsync(message.Attachments.First().Url).ContinueWith(task =>
        {
            var log = Encoding.UTF8.GetString(task.Result);
            if (!IsLog(log))
            {
                return Task.CompletedTask;
            }
            
            new LogScanner(log, message).PerformScan();
            return Task.CompletedTask;
        }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.RunContinuationsAsynchronously);
        
        return Task.CompletedTask;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsLog(string log) => log.Contains("MelonLoader v") && log.Contains("BloonsTD6");
}