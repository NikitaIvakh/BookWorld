using System.Reflection;

namespace Coupons.API.AssemblyReferences;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}