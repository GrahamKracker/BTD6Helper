namespace BTD6Helper.Errors;

public class DuplicateNameSpaces : Error
{
    protected override string Message => "2 mods are conflicting with each other because they use the same namespace. (this is a common problem with DatJaneDoe's old mods)";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("inject the same type twice, or use a different namespace");
    }
}