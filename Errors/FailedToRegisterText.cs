namespace BTD6Helper.Errors;

public class FailedToRegisterText : Error
{
    protected override string Message => "A custom tower/paragon mod failed to load, it's probably broken/outdated.";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("[BloonsTD6_Mod_Helper] Failed to register text");
    }
}