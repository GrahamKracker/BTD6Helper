namespace BTD6Helper.Errors;

public partial class OutdatedMods : Error
{
    public override int Priority => 1;
    protected override string Message => _message;
    private string _message = "";
    protected override bool ShouldAppendNewline => false;
    
    public override bool AffectsLog(string log, SocketMessage message)
    {
        var stringBuilder = new StringBuilder();
        var found = false;
        foreach (Match match in MissingDependenciesRegex().Matches(log))
        {
            found = true;
            stringBuilder.Append($"`{match.Groups[1].Value}` is outdated and will not work. Make sure to check mod browser or the modding servers for a new version. Reinstalling MelonLoader with your antivirus turned off **may** help fix the issue.\n- ");
        }
        
        foreach (Match match in TypeLoadRegex().Matches(log))
        {
            found = true;
            stringBuilder.Append($"`{match.Groups[1].Value}` is outdated and will not work. Make sure to check mod browser or the modding servers for a new version. Reinstalling MelonLoader with your antivirus turned off **may** help fix the issue.\n- ");
        }
        
        foreach (Match match in AssemblyLoadRegex().Matches(log))
        {
            found = true;
            stringBuilder.Append($"`{match.Groups[1].Value}` failed to load and will not work. Make sure to check mod browser or the modding servers for a new version. Reinstalling MelonLoader with your antivirus turned off **may** help fix the issue.\n- ");
        }

        _message = stringBuilder.ToString();
        return found;
    }

    [GeneratedRegex(@"- '(.*?)' is missing the following dependencies:")]
    private static partial Regex MissingDependenciesRegex();
    
    [GeneratedRegex(@"Failed to load all types in assembly (.*?), ")]
    private static partial Regex TypeLoadRegex();
    
    [GeneratedRegex(@"Failed to load Melon Assembly from '.*?\\Mods\\(.*?)\':")]
    private static partial Regex AssemblyLoadRegex();
}