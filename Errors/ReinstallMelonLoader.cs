namespace BTD6Helper.Errors;

public class ReinstallMelonLoader : Error
{
    protected override string Message => "Reinstall MelonLoader using the [guide](https://hemisemidemipresent.github.io/btd6-modding-tutorial/). (Make sure to delete the existing Melonloader files first).";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("System.ComponentModel.Win32Exception") || log.Contains("get_RegisteredMelons") ||
               log.Contains("MelonLoader.MelonHandler") ||
               log.Contains("[INTERNAL FAILURE] Failed to Process UnityDependencies!") ||
               log.Contains("Critical failure when loading resources for mod BloonsTD6 Mod Helper") ||
               log.Contains("[ERROR] No Support Module Loaded!") ||
               log.Contains("Could not resolve type with token");
    }
}