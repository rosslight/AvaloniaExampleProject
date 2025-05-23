using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AvaloniaExampleProject.Business;

public interface IAppInformationService
{
    string Version { get; }
}

public sealed class AppInformationService : IAppInformationService
{
    [field: AllowNull, MaybeNull]
    public string Version
    {
        get
        {
            field ??=
                typeof(App).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                ?? throw new VersionNotFoundException("Could not get version");
            return field!;
        }
    }
}
