using System.Reflection;

namespace Coupons.Domain.AssemblyReferences;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}