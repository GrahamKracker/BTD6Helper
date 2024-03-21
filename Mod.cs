namespace BTD6Helper;

public readonly struct Mod(string? name, string? version, string? author, string assembly)
{
    private string? Name { get; } = name;
    private string? Author { get; } = author;
    private string? Version { get; } = version;
    private string Assembly { get; } = assembly;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(Name ?? Assembly);
        if (Version != null)
            sb.Append($" v{Version}");
        if (Author != null)
            sb.Append($" by {Author}");
        return sb.ToString();
    }
}