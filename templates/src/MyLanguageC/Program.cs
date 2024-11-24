using Spectre.Console.Cli;

namespace MyLanguageC;

public static class Program
{
    public static int Main(string[] args)
    {
        var app = new CommandApp<CompileCommand>();
        return app.Run(args);
    }
}