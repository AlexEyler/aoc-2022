﻿using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Eyler.AdventOfCode._2022.SourceGenerator;

[Generator]
public class DayDiscoverGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        Debugger.Launch();
        if (context.SyntaxContextReceiver is DayFinder dayFinder)
        {
            var days = dayFinder.Days;
            string source = $@"// <auto-generated/>
using System;
using System.Collections.Generic;

namespace Eyler.AdventOfCode._2022;

public partial class DayDiscoverer
{{
    private IDictionary<string, Func<IDay>> DayMapping = new Dictionary<string, Func<IDay>>(StringComparer.OrdinalIgnoreCase)
    {{
        {string.Join("\n", days.Select(d => $"{{ \"{d.Name}\", () =>  new {d.ToDisplayString()}() }}"))}
    }};
}}";
            context.AddSource("DayDiscoverer.Map.g.cs", source);
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        Debugger.Launch();
        context.RegisterForSyntaxNotifications(() => new DayFinder());
    }

    private class DayFinder : ISyntaxContextReceiver
    {
        public List<INamedTypeSymbol> Days { get; } = new List<INamedTypeSymbol>();
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = context.Node as ClassDeclarationSyntax;
            if (classDeclarationSyntax != null)
            {
                var model = context.SemanticModel;
                var classSymbol = model.GetDeclaredSymbol(classDeclarationSyntax) as INamedTypeSymbol;

                if (classSymbol != null && classSymbol.AllInterfaces.Any(i => i.ToDisplayString().EndsWith("IDay")))
                {
                    Days.Add(classSymbol);
                }
            }
        }
    }
}

