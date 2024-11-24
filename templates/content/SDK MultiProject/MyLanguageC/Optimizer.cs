using DistIL.Passes;

namespace MyLanguageC;

public class Optimizer
{
    private readonly Dictionary<string, OptimizationLevel> _levels;

    public Optimizer()
    {
        _levels = new Dictionary<string, OptimizationLevel>
        {
            { "O0", new OptimizationLevel("O0") },
            { "O1", new OptimizationLevel("O1") },
            { "O2", new OptimizationLevel("O2") },
            { "O3", new OptimizationLevel("O3") }
        };

        // Define passes
        foreach (var level in _levels.Values)
        {
            level.AddPass(new DeadCodeElim());
            level.AddPass(new SimplifyCFG());
        }
    }

    public void SetOptimizationLevel(string level)
    {
        if (_levels.TryGetValue(level, out var level1))
        {
            Console.WriteLine($"Setting optimization level: {level}");
            // Enable all passes for this level
            foreach (var pass in level1.Passes)
            {
                pass.IsEnabled = true;
            }
        }
        else
        {
            Console.WriteLine("Invalid optimization level.");
        }
    }

    public void ExcludePass(string passName)
    {
        foreach (var level in _levels.Values)
        {
            var pass = level.Passes.Find(p => p.Name.Equals(passName, StringComparison.OrdinalIgnoreCase));
            if (pass != null)
            {
                pass.IsEnabled = false;
                Console.WriteLine($"Excluded pass: {passName} from level: {level.Level}");
            }
        }
    }

    public void DisplayActivePasses()
    {
        foreach (var level in _levels.Values)
        {
            Console.WriteLine($"Optimization Level: {level.Level}");
            foreach (var pass in level.Passes)
            {
                if (pass.IsEnabled)
                {
                    Console.WriteLine($"  - Active Pass: {pass.Name}");
                }
                else
                {
                    Console.WriteLine($"  - Excluded Pass: {pass.Name}");
                }
            }
        }
    }
}