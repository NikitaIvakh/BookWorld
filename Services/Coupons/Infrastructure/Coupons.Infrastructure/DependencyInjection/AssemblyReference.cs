using System.Reflection;

namespace Coupons.Infrastructure.DependencyInjection;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}