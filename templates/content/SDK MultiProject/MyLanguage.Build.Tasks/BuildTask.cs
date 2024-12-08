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

    public ITaskItem[] EmbeddedResources { get; set; }

    public string OptimizeLevel { get; set; }
    public bool DebugSymbols { get; set; }
    public string Configuration { get; set; }
    public string Version { get; set; }
    public string RootNamespace { get; set; }

    public override bool Execute()
    {
        var driver = Driver.Create(new DriverSettings
        {
            OutputPath = OutputPath,
            RootNamespace = RootNamespace,
            Sources = SourceFiles.Select(_ => _.ItemSpec).ToArray(),
            EmbeddedResources = EmbeddedResources.Select(_ => _.ItemSpec).ToArray(),
            OptimizeLevel = OptimizeLevel,
            DebugSymbols = DebugSymbols,
            IsDebug = Configuration == "Debug",
            Version = Version
        });

        foreach (var reference in ReferencePaths)
            try
            {
                // Driver.moduleResolver.Load(reference.ItemSpec);
            }
            catch
            {
            }

        foreach(var er in EmbeddedResources)
        {
            //ToDo: implement embeddedresources
            /*
```cs
        var resource1 = asm.CreateEmbeddedResource("data1", [1, 2, 3, 4, 5, 6, 7, 8, 9]);
        var resource2 = new LinkedResource(asm, "ref2", System.Reflection.ManifestResourceAttributes.Public, "lorem_ipsum.txt", []);
        asm.ManifestResources.Add(resource2);
```
            */
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