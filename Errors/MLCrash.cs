namespace BTD6Helper.Errors;

public partial class MLCrash : Error
{
    protected override string Message => "MelonLoader crashed, try reinstalling MelonLoader with your antivirus turned off";

    public override bool AffectsLog(string log, SocketMessage message) => log.Split('\n')[^1].Contains("SHA256 Hash:");
}