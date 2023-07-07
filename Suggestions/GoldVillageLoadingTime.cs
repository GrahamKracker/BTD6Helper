namespace BTD6Helper.Suggestions;

public class GoldVillageLoadingTime : Suggestion
{
    protected override string Message => "GoldVillage takes a long time to load on start, it may look like your game is crashing/freezing, but you just need to wait.";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("GoldVillage");
    }
}