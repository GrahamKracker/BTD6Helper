namespace BTD6Helper.Errors;

public class UnhollowerDependency : Error
{
    protected override string Message => "Mods that were last updated before December 26th, 2022 are all broken";
    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("SHA256 Hash") && log.Contains("Melon Assembly loaded") && log.Contains("is missing the following dependencies") && log.Contains("UnhollowerBaseLib");
    }
}