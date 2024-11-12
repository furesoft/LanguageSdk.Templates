﻿namespace TestCompiler;

public class Program
{
    public static void Main()
    {
        var driver = new Driver();
        driver.Sources = ["test.my"];
        driver.IsDebug = true;
        driver.OutputPath = "compiled.dll";
        driver.DebugSymbols = true;

        var documents = driver.Compile();
    }
}