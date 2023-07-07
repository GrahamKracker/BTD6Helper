namespace BTD6Helper.Errors;

public class GurrenCore : Error
{
    protected override string Message => "Gurren Core and all the mods that rely on it are broken";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("Gurren Core");
    }
}