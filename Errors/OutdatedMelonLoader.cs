namespace BTD6Helper.Errors;

public class OutdatedMelonLoader : Error
{
    public override int Priority => 100;
    protected override string Message => "***You need MelonLoader v0.6.1, install it following the guide [here](https://hemisemidemipresent.github.io/btd6-modding-tutorial/). (Make sure to delete the existing Melonloader files first)***";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("MelonLoader v0.5.") || log.Contains("MelonLoader v0.6.0");
    }
}