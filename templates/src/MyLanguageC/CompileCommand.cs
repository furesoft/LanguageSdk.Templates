using Spectre.Console.Cli;

namespace MyLanguageC;

public class CompileCommand : Command<DriverSettings>
{
    public override int Execute(CommandContext context, DriverSettings settings)
    {
        var driver = new Driver
        {
            Settings = settings
        };

        driver.Compile();

        return 0;
    }
}