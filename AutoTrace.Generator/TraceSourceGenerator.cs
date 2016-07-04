using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace AutoTrace.Generator
{
    [SourceGenerator("C#")]
    public class TraceSourceGenerator : SourceGenerator
    {
        public override void Execute(SourceGeneratorContext context)
        {
            //System.Diagnostics.Debugger.Launch();
            var visitor = new NamedTypeSymbolCodeGenerator(context, new MethodSourceGenerator(), new PropertySourceGenerator());
            visitor.Visit(context.Compilation.SourceModule);
        }
    }
}
