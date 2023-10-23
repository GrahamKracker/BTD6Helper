namespace BTD6Helper.Errors;

public class EOSBootStrapperCrash : Error
{
    protected override string Message => "Navigate to the BTD6 files and open `EOSBootStrapper.exe`. When the game loads and you get stuck on the loading screen, close it and open the game from the Epic Games Launcher";
    public override bool AffectsLog(string log, SocketMessage message) => log.Split('\n')[^1].Contains("1 Plugin loaded.");
}