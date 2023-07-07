namespace BTD6Helper.Errors;

public class NkHook : Error
{
    protected override string Message => "NKHook6 and all the mods that rely on it are broken";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("NKHook6");
    }
}