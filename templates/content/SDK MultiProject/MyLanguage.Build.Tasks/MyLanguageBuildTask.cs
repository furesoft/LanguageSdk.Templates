using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using MyLanguageC;
using Silverfly.Text;

namespace MyLanguage.Build.Tasks;

public class MyLanguageBuildTask : LanguagseSdk.Templates.Core.BuildTask
{
    
    public override bool Execute(DriverSettings settings)
    {
        var driver = Driver.Create(settings);

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