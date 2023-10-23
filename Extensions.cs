using System;

namespace BTD6Helper;

public static class Extensions
{
    public static IEnumerable<Type> GetValidTypes(
        this Assembly asm)
    {
        IEnumerable<Type> returnval;
        try
        {
            returnval = asm.GetTypes().AsEnumerable();
        }
        catch (ReflectionTypeLoadException ex) 
        {
            returnval = ex.Types!;
        }

        return returnval.Where(x => x is not null);
    }
}