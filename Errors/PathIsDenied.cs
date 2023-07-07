namespace BTD6Helper.Errors;

public class PathIsDenied : Error
{
    protected override string Message => "Reinstall MelonLoader using the [guide](https://hemisemidemipresent.github.io/btd6-modding-tutorial/). (Make sure to delete the existing Melonloader files first).";
    
    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("Access to the path") && log.Contains("is denied");
    }
}