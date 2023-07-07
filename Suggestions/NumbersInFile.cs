namespace BTD6Helper.Suggestions;

public partial class NumbersInFile : Suggestion
{
    protected override bool ShouldAppendNewline => false;
    protected override string Message => _message;
    private string _message = "";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        var stringBuilder = new StringBuilder();
        var found = false;
        foreach (Match match in NumberRegex().Matches(log))
        {
            found = true;
            stringBuilder.Append($"Remove the numbers at the end of the file: `{match.Groups[1].Value}`\n- ");
        }
        _message = stringBuilder.ToString();
        return found;
    }

    [GeneratedRegex(@"\\Mods\\(.*? \(\d+\)\.dll)")]
    private static partial Regex NumberRegex();
}