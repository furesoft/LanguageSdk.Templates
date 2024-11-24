using DistIL;
using Spectre.Console.Cli;

namespace MyLanguageC;

public class CompileCommand : Command<DriverSettings>
{
    public override int Execute(CommandContext context, DriverSettings settings)
    {
        var driver = Driver.Create(settings);

        driver.Compile();

        return 0;
    }
}