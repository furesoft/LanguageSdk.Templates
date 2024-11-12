using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Silverfly;

namespace TestCompiler;

public class Driver
{
    public bool DebugSymbols = false;
    public bool IsDebug = false;
    public bool Optimize = false;
    public string[] Sources;
    public string OutputPath { get; set; } = "compiled.dll";
    public string RootNamespace { get; set; } = "";
    public Version Version { get; set; } = new(1, 0);

    public Silverfly.Text.SourceDocument[] Compile()
    {
       //ToDo: implement compilation
    }
}