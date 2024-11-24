using DistIL.AsmIO;
using Silverfly.Text;

namespace MyLanguageC;

public class Driver
{
    public DriverSettings Settings { get; set; } = new();
    public ModuleResolver ModuleResolver { get; set; }

    public SourceDocument[] Compile()
    {
        // ToDo: implement compilation
        return [];
    }
}