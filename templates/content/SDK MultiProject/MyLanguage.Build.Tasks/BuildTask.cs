using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using MyLanguageC;
using Silverfly.Text;

namespace MyLanguage.Build.Tasks;

public class BuildTask : Task
{
    [System.ComponentModel.DataAnnotations.Required]
    public ITaskItem[] SourceFiles { get; set; }

    [System.ComponentModel.DataAnnotations.Required]
    public string OutputPath { get; set; }

    [System.ComponentModel.DataAnnotations.Required]
    public ITaskItem[] ReferencePaths { get; set; }

    public bool Optimize { get; set; }
    public bool DebugSymbols { get; set; }
    public string Configuration { get; set; }
    public string Version { get; set; }
    public string RootNamespace { get; set; }

    public override bool Execute()
    {
        var driver = new Driver();

        driver.ModuleResolver.AddTrustedSearchPaths();

        driver.Settings.OutputPath = OutputPath;
        driver.Settings.RootNamespace = RootNamespace;
        driver.Settings.Sources = SourceFiles.Select(_ => _.ItemSpec).ToArray();
        driver.Settings.Optimize = Optimize;
        driver.Settings.DebugSymbols = DebugSymbols;
        driver.Settings.IsDebug = Configuration == "Debug";
        driver.Settings.Version = Version;

        foreach (var reference in ReferencePaths)
            try
            {
                // Driver.moduleResolver.Load(reference.ItemSpec);
            }
            catch
            {
            }

        var documents = driver.Compile();

        foreach (var message in documents.SelectMany(_ => _.Messages))
        {
            switch (message.Severity)
            {
                case MessageSeverity.Error:
                    Log.LogError(null, null, null,
                        file: message.Document.Filename, message.Range.Start.Line, message.Range.Start.Column,
                        message.Range.End.Line, message.Range.End.Column, message.Text);
                    break;
                case MessageSeverity.Warning:
                    Log.LogWarning(null, null, null,
                        file: message.Document.Filename, message.Range.Start.Line, message.Range.Start.Column,
                        message.Range.End.Line, message.Range.End.Column, message.Text);
                    break;
            }
        }

        return true;
    }
}