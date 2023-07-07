namespace BTD6Helper.Errors;

public class ProxyIssues : Error
{
    protected override string Message => "A Melonloader file failed to download, Open the \"Change Proxy Settings\" settings window and disable all three toggles.";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("Failed to Download UnityDependencies!") || log.Contains("System.Net.Sockets.SocketException");
    }
}