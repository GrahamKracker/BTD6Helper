namespace BTD6Helper.Errors;

public class RemoveAntivirus : Error
{
    protected override string Message => "Your antivirus deleted a melonloader file. Reinstall melonloader following the [guide](https://hemisemidemipresent.github.io/btd6-modding-tutorial/) and add an exception to your antivirus. Make sure to delete the existing Melonloader files first.";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return !log.Contains("Preferences Loaded!") || log.Contains("Operation did not complete successfully because the file contains a virus or potentially unwanted software.");
    }
}