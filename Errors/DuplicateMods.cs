namespace BTD6Helper.Errors;

public class DuplicateMods : Error
{
    protected override string Message => "You have duplicate mods. Remove the duplicate mods and try again.";

    public override bool AffectsLog(string log, SocketMessage message) => log.Contains("Assembly with same name is already loaded");
}