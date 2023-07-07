namespace BTD6Helper;

public abstract class Error
{
    protected virtual bool ShouldAppendNewline => true;
    protected abstract string Message { get; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => ShouldAppendNewline ? Message + "\n- " : Message;
    
    public virtual int Priority => 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract bool AffectsLog(string log, SocketMessage message);
}