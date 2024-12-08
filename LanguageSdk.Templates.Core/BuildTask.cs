namespace LanguageSdk.Templates.Core;

using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

public abstract class BuildTask : Task
{
    [System.ComponentModel.DataAnnotations.Required]
    public ITaskItem[] SourceFiles { get; set; }

    [System.ComponentModel.DataAnnotations.Required]
    public string OutputPath { get; set; }

    [System.ComponentModel.DataAnnotations.Required]
    public ITaskItem[] ReferencePaths { get; set; }

    public ITaskItem[] EmbeddedResources { get; set; }

    public string OptimizeLevel { get; set; }
    public bool DebugSymbols { get; set; }
    public string Configuration { get; set; }
    public string Version { get; set; }
    public string RootNamespace { get; set; }

    public override bool Execute()
    {
        var settings = new DriverSettings
        {
            OutputPath = OutputPath,
            RootNamespace = RootNamespace,
            Sources = SourceFiles.Select(_ => _.ItemSpec).ToArray(),
            EmbeddedResources = EmbeddedResources.Select(_ => _.ItemSpec).ToArray(),
            OptimizeLevel = OptimizeLevel,
            DebugSymbols = DebugSymbols,
            IsDebug = Configuration == "Debug",
            Version = Version
        };

        return true;
    }

    protected abstract void Execute(DriverSettings settings);
}