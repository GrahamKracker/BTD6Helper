namespace BTD6Helper.Errors;

public class NoModHelper : Error
{
    protected override string Message => "You need mod helper. Download it [here.](https://github.com/gurrenm3/BTD-Mod-Helper/releases/latest/download/Btd6ModHelper.dll)";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("SHA256 Hash") && log.Contains("Melon Assembly loaded") && log.Contains("Loading Mods from") && !log.Contains("Btd6ModHelper");
    }
}