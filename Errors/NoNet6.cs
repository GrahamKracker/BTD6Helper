namespace BTD6Helper.Errors;

public class NoNet6 : Error
{
    public override int Priority => 100;
    protected override string Message => "***Install [.NET 6 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-6.0.12-windows-x64-installer).***";

    public override bool AffectsLog(string log, SocketMessage message)
    {
        return log.Contains("il2cpp_init detour failed");
    }
}