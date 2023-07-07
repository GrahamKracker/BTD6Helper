namespace BTD6Helper.Errors;

public class NexusOudatedModHelper : Error
{
    protected override string Message => "You got the mod helper from nexus. That version is completely outdated, get the new one [here](https://github.com/gurrenm3/BTD-Mod-Helper/releases/latest/download/Btd6ModHelper.dll)";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("BloonsTD6 Mod Helper v2.3.1");
    }
}