using System.Reflection;

namespace Coupons.Persistence.DependencyInjection;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}