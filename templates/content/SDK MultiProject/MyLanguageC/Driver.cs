using DistIL;
using DistIL.AsmIO;
using Silverfly.Text;

namespace MyLanguageC;

public class Driver
{
    public DriverSettings Settings { get; set; } = new();
    public required Compilation Compilation { get; set; }

    public Optimizer Optimizer { get; set; }

    public static Driver Create(DriverSettings settings)
    {
        var moduleResolver = new ModuleResolver();
        var module = moduleResolver.Create(settings.RootNamespace, Version.Parse(settings.Version));
        moduleResolver.AddTrustedSearchPaths();

        return new Driver
        {
            Compilation = new Compilation(module, new ConsoleLogger(), new CompilationSettings()),
            Settings = settings,
            Optimizer = new Optimizer()
        };
    }

    public SourceDocument[] Compile()
    {

        // ToDo: implement compilation
        return [];
    }
}