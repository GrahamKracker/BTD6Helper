namespace BTD6Helper;

public sealed record Mod(string? Name, string? Version, string? Author, string Assembly)
{
    public string? Name { get; } = Name;
    public string? Author { get; } = Author;
    public string? Version { get; } = Version;
    public string Assembly { get; } = Assembly;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(Name?? Assembly);
        if (Version != null)
            sb.Append($" v{Version}");
        if (Author != null)
            sb.Append($" by {Author}");
        return sb.ToString();
    }
}