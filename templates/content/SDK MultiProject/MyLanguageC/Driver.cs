using DistIL;
using DistIL.AsmIO;
using Silverfly.Text;
using LanguageSdk.Templates.Core;

namespace MyLanguageC;

public class Driver
{
    public DriverSettings Settings { get; set; } = new();
    public required Compilation Compilation { get; set; }

    public Optimizer Optimizer { get; set; }

    public static Driver Create(DriverSettings settings)
    {
        var moduleResolver = new ModuleResolver();
        moduleResolver.AddTrustedSearchPaths();

        var module = moduleResolver.Create(settings.RootNamespace, Version.Parse(settings.Version));

        var compilation = new Compilation(module, new ConsoleLogger(), new CompilationSettings());
        var optimizer = new Optimizer();
        optimizer.CreatePassManager(compilation);

        return new Driver
        {
            Compilation = compilation,
            Settings = settings,
            Optimizer = optimizer
        };
    }

    public SourceDocument[] Compile()
    {

        // ToDo: implement compilation
        return [];
    }
}