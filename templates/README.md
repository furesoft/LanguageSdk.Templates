# LanguageSdk.Templates

A basic skeleton to build a custom msbuild sdk for custom language.

Features:

- SDK
- Compiler project as dotnet tool
- Resolve references (project, nuget) automatically
- Optimizer that handle enabling/disabling optimization passes and use optimization levels
- Optimize on release configuration

## Usage
````shell
dotnet new lsdk -l:MyLanguage -ext:.my
````