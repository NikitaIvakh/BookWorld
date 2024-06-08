using System.Reflection;

namespace Coupons.Application.DependencyInjection;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}