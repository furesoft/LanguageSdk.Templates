using DistIL.Passes;

namespace LanguageSdk.Templates.Core;

public class OptimizationPass(string name)
{
    public string Name { get; } = name;
    public bool IsEnabled { get; set; } = true;

    public IMethodPass Pass { get; set; }
}