namespace BTD6Helper.Errors;

public class GurrenMods : Error
{
    protected override string Message => "Gurren's old mods are broken";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("MainMenu_OnEnable::Postfix()");
    }
}