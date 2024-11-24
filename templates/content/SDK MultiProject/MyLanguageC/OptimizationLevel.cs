namespace MyLanguageC;

public class OptimizationLevel(string level)
{
    public string Level { get; } = level;
    public List<OptimizationPass> Passes { get; } = new();

    public void AddPass(OptimizationPass pass)
    {
        Passes.Add(pass);
    }
}