using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Repository.Extension.ServiceExtensions;

public static class TypeExtensions
{
    public static IEnumerable<Assembly> GetCurrentPathAssembly(this AppDomain domain)
    {
        var dlls = DependencyContext.Default!.CompileLibraries
            .Where(x => !x.Name.StartsWith("Microsoft") && !x.Name.StartsWith("System"))
            .ToList();
        var list = new List<Assembly>();
        if (dlls.Count != 0)
        {
            list.AddRange(from dll in dlls where dll.Type == "project" select Assembly.Load(dll.Name));
        }
        return list;
    }

    public static bool HasImplementedRawGeneric(this Type type, Type generic)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(generic);
        var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
        if (isTheRawGenericType) return true;
        while (type != null && type != typeof(object))
        {
            isTheRawGenericType = IsTheRawGenericType(type);
            if (isTheRawGenericType) return true;
            type = type.BaseType;
        }
        return false;

        bool IsTheRawGenericType(Type test)
            => generic == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
    }
}