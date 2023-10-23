using System;

namespace BTD6Helper;

public sealed partial class LogScanner
{
    private static readonly AllowedMentions AllowedMentions = new(AllowedMentionTypes.None);

    private static readonly HashSet<Error> AllErrors = Assembly.GetExecutingAssembly().GetValidTypes()
        .Where(type => type.BaseType == typeof(Error) && CanLoadType(type))
        .Select(type => (Error)Activator.CreateInstance(type)!).ToHashSet();

    private static readonly HashSet<Suggestion> AllSuggestions = Assembly.GetExecutingAssembly().GetValidTypes()
        .Where(type => type.BaseType == typeof(Suggestion) && CanLoadType(type))
        .Select(type => (Suggestion)Activator.CreateInstance(type)!).OrderBy(suggestion => suggestion.Priority)
        .ToHashSet();

    private static bool CanLoadType(Type type) => type is { IsAbstract: false, ContainsGenericParameters: false } &&
        type.GetConstructor(
        BindingFlags.Instance | BindingFlags.Public |
        BindingFlags.NonPublic, null, Type.EmptyTypes, null) != null;

    #region Colors
    private static readonly Color NoIssueColor = new(0x00ff00);
    private static readonly Color SuggestionColor = new(0xffff00);
    private static readonly Color ErrorColor = new(0xff0000);
    #endregion
    
    private readonly List<Error> _errors;
    private readonly List<Suggestion> _suggestions;

    private readonly HashSet<Mod> _mods = new();
    private readonly SocketMessage _message;
    private readonly EmbedBuilder _embedBuilder;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LogScanner(string log, SocketMessage message)
    {
        var lines = log.Split('\n').SkipWhile(line => !line.Contains("Loading Mods from")).ToArray();
        
        for(var i = 0; i < lines.Length - 2; i++)
        {
            var line = lines[i];
            var versionMatch = VersionRegex().Match(line);
            var authorMatch = AuthorRegex().Match(lines[i + 1]);
            var dllMatch = DllRegex().Match(lines[i + 2]);
            if (versionMatch.Success && authorMatch.Success && dllMatch.Success)
            {
                _mods.Add(new Mod(versionMatch.Groups[1].Value, versionMatch.Groups[2].Value, authorMatch.Groups[1].Value, dllMatch.Groups[1].Value));
            }
        }

        if (_mods.Count == 0)
        {
            foreach (Match match in MelonAssemblyRegex().Matches(log))
            {
                _mods.Add(new Mod(null, null, null, match.Groups[1].Value));
            }
        }
        
        
        _message = message;
        _errors = AllErrors.Where(error => error.AffectsLog(log, _message)).OrderByDescending(error => error.Priority).ToList();
        _suggestions = AllSuggestions.Where(suggestion => suggestion.AffectsLog(log, _message)).OrderByDescending(suggestion => suggestion.Priority).ToList();
        
        Color? color;
        if (_errors.Count > 0)
        {
            color = ErrorColor;
        }
        else
        {
            color = NoIssueColor;
            if (_suggestions.Count > 0)
                color = SuggestionColor;
        }
        
        _embedBuilder = new EmbedBuilder
        {
            Title = "Log Results:",
            Description = "Contact <@!699262005529542678> if there are any false detections.",
            ThumbnailUrl = "https://images-ext-2.discordapp.net/external/r2THoHnoRQQN2p6N9vnpTK29tMIbt0bPMHBG4Mkd3kE/https/i.imgur.com/BSXtkvW.png?width=1049&height=617",
            Color = color,
            Timestamp = DateTimeOffset.UtcNow,
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PerformScan()
    {
        _embedBuilder.Footer = new EmbedFooterBuilder
        {
            Text =
                $"Created by GrahamKracker and Timotheeee1 | {_mods.Count} mod{(_mods.Count == 1 ? "" : "s")} detected"
        };
        
        if (_errors.Count > 0)
        {
            var errorsAsString = _errors.Select(error => error.ToString()).Aggregate((a, b) => a + b);
            errorsAsString = errorsAsString.Remove(errorsAsString.Length - 2);
            _embedBuilder.AddField("Errors: ", $"- {errorsAsString}");
        }
        
        if (_suggestions.Count > 0)
        {
            var suggestionsAsString = _suggestions.Select(error => error.ToString()).Aggregate((a, b) => a + b);
            suggestionsAsString = suggestionsAsString.Remove(suggestionsAsString.Length - 2);
            _embedBuilder.AddField("Suggestions: ", $"- {suggestionsAsString}");
        }
        
        if (_mods.Count is 0)
            _embedBuilder.AddField("Mods: ", "No mods detected");
        else
        {
            var mods = _mods.Select<Mod, string>(mod => mod.ToString()).Aggregate((a, b) =>
            {
                b = $"\n- {b}";
                const string tooManyMods = "**Too many mods to list, please check the log.**";
                if (a.Length + b.Length > EmbedFieldBuilder.MaxFieldValueLength - 2 - tooManyMods.Length)
                {
                    return a.Contains(tooManyMods) ? a : $"{a}\n- {tooManyMods}";
                }
                return a + b;
            });
            _embedBuilder.AddField("Mods: ",
                $"- {mods}");
        }

        _message.Channel.SendMessageAsync(messageReference: new MessageReference(_message.Id),
            allowedMentions: AllowedMentions, embed:_embedBuilder.Build());
    }

    [GeneratedRegex(@"Melon Assembly loaded: '.\\Mods\\(.*?)'")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static partial Regex MelonAssemblyRegex();
    
    [GeneratedRegex(@"Assembly: (.*?)\r")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static partial Regex DllRegex();
    
    [GeneratedRegex(@".*? by (.*?)\r")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static partial Regex AuthorRegex();

    [GeneratedRegex(@" (.*?) v(\d+\.\d+\.\d+)")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static partial Regex VersionRegex();
}