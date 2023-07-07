namespace BTD6Helper.Errors;

public class BrokenDisplays : Error
{
    protected override string Message => "A lot of custom tower mods broke in a recent BTD6 update. Remove the ones that are giving errors.";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("get_display");
    }
}