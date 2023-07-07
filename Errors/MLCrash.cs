namespace BTD6Helper.Errors;

public partial class MLCrash : Error
{
    protected override string Message => "MelonLoader crashed, send a screenshot of any text in the console that doesnt appear in the log. Try reinstalling MelonLoader with your antivirus turned off";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.EndsWith(HashRegex().Matches(log)[^1].Value);
    }
    
    [GeneratedRegex(@"SHA256 Hash: '.*?'")]
    private static partial Regex HashRegex();
}