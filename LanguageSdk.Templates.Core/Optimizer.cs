using DistIL;
using DistIL.AsmIO;
using DistIL.Passes;

namespace LanguageSdk.Templates.Core;

public class Optimizer
{
    private readonly Dictionary<string, OptimizationLevel> _levels = [];

    public PassManager PassManager { get; set; }

    public void DefineOptimizationLevel(string name, Action<OptimizationLevel> builder)
    {
        var optLevel = new OptimizationLevel(name);
        builder(optLevel);

        _levels.Add(name, optLevel);
    }

    public void DefineDefaults()
    {
        DefineOptimizationLevel("O0", lvl =>
        {

        });

        DefineOptimizationLevel("O1", lvl =>
        {
            lvl.AddPass(new DeadCodeElim());
            lvl.AddPass(new SimplifyCFG());
        });

        DefineOptimizationLevel("O2", lvl =>
        {
            ApplyFromOtherPass("O1", lvl);

            lvl.AddPass(new AssertionProp());
            lvl.AddPass(new InlineMethods());
            lvl.AddPass(new PresizeLists());
        });

        DefineOptimizationLevel("O3", lvl =>
        {
            ApplyFromOtherPass("O2", lvl);

            lvl.AddPass(new ScalarReplacement());
            lvl.AddPass(new ValueNumbering());
            lvl.AddPass(new LoopStrengthReduction());
            lvl.AddPass(new SsaPromotion());
        });
    }

    public void ApplyFromOtherPass(string name, OptimizationLevel lvl)
    {
        _levels[name].Passes.ForEach(p => lvl.AddPass(p.Pass));
    }

    public void SetOptimizationLevel(string level)
    {
        if (_levels.TryGetValue(level, out var level1))
        {
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
                Console.WriteLine(pass.IsEnabled ? $"  - Active Pass: {pass.Name}" : $"  - Excluded Pass: {pass.Name}");
            }
        }
    }

    public void CreatePassManager(Compilation compilation, DriverSettings settings)
    {
        var pm = new PassManager
        {
            Compilation = compilation
        };

        var passes = pm.AddPasses();

        foreach (var pass in _levels[settings.OptimizeLevel].Passes)
        {
            if (pass.IsEnabled)
            {
                passes.Apply(pass.Pass);
            }
        }

        PassManager = pm;
    }

    public void Run(IReadOnlyCollection<MethodDef> methods)
    {
        PassManager.Run(methods);
    }
}