using Spectre.Console.Cli;

namespace MyLanguageC;

public class DriverSettings : CommandSettings
{
    [CommandOption("--debug-symbols")]
    public bool DebugSymbols { get; set; }

    [CommandOption("--is-debug")]
    public bool IsDebug { get; set; }

    [CommandOption("--optimize")]
    public bool Optimize { get; set; }

    [CommandArgument(0, "[sources]")]
    public string[] Sources { get; set; } = [];

    [CommandOption("--output-path")]
    public string OutputPath { get; set; } = "compiled.dll";

    [CommandOption("--root-namespace")]
    public string RootNamespace { get; set; } = "";

    [CommandOption("--version")]
    public string Version { get; set; } = "1.0";
}