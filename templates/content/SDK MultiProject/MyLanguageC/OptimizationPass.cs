namespace MyLanguageC;

public class OptimizationPass(string name)
{
    public string Name { get; } = name;
    public bool IsEnabled { get; set; } = true;
}