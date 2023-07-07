using System;

namespace BTD6Helper;

public static class Extensions
{
    public static IEnumerable<Type> GetValidTypes(
        this Assembly asm)
    {
        var returnval = Enumerable.Empty<Type>();
        try
        {
            returnval = asm.GetTypes().AsEnumerable();
        }
        catch (ReflectionTypeLoadException ex) 
        {
            Console.WriteLine($"Failed to load all types in assembly {asm.FullName} due to: {ex.Message}", ex);
            returnval = ex.Types!;
        }

        return returnval.Where(x => x != null);
    }
}