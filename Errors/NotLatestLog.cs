namespace BTD6Helper.Errors;

public class NotLatestLog : Error
{
    public override int Priority => -1;

    protected override string Message =>
        "Make sure you are sending the latest log, it can be found within the MelonLoader folder, which is in the BTD6 folder, as a file named `Latest.log`. You can find out more information by typing `!log`.";
    public override bool AffectsLog(string log, SocketMessage message) => message.Attachments.First().Filename != "Latest.log";
}