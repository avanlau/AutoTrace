using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AutoTrace.Generator
{
    public class NamedTypeSymbolCodeGenerator : TraceSymbolVisitorBase
    {
        private readonly CSharpParseOptions _parseOptions;
        private readonly IMethodSourceGenerator _methodSourceGenerator;
        private readonly IPropertySourceGenerator _propertySourceGenerator;

        public NamedTypeSymbolCodeGenerator(SourceGeneratorContext context, IMethodSourceGenerator methodSourceGenerator, IPropertySourceGenerator propertySourceGenerator) : base(context)
        {
            _parseOptions = CSharpParseOptions.Default.WithFeatures(_comp.SyntaxTrees.First().Options.Features);
            _methodSourceGenerator = methodSourceGenerator;
            _propertySourceGenerator = propertySourceGenerator;
        }

        public override void ProcessType(INamedTypeSymbol symbol)
        {
            var generatedSource = GenerateSource(symbol);

            if (!generatedSource.Equals(string.Empty))
            {
                generatedSource = AddTypeSource(symbol, generatedSource);
                AddSourceToCompilation(symbol.Name, generatedSource);
            }
        }

        private string AddTypeSource(INamedTypeSymbol symbol, string generatedSource)
        {
            string code = $@"
using System;
using AutoTrace;

namespace {symbol.ContainingNamespace.ToString()}
{{
    {symbol.DeclaredAccessibility.ToString().ToLower()} partial class {symbol.Name}
    {{
        {generatedSource}
    }}
}}
";
            return code;
        }

        private void AddSourceToCompilation(string symbolName, string generatedSource)
        {
            var source = SyntaxFactory.ParseSyntaxTree(generatedSource.ToString(), encoding: Encoding.UTF8, options: _parseOptions);

            var normalized = source.GetRoot().NormalizeWhitespace();
            source = SyntaxFactory.SyntaxTree(normalized, _parseOptions, encoding: Encoding.UTF8);
            _context.AddCompilationUnit($"{symbolName}.AutoTrace", source);
        }

        private string GenerateSource(INamedTypeSymbol symbol)
        {
            StringBuilder sb = new StringBuilder();

            // Properties
            var properties = symbol.GetMembers().Where(m => m.Kind == SymbolKind.Property);
            foreach (IPropertySymbol property in properties)
            {
                if (HasTraceAttribute(property) || HasTraceAttribute(property.ContainingType) || HasTraceAttribute(property.ContainingNamespace) || HasTraceAttribute(property.ContainingAssembly) || HasTraceAttribute(property.ContainingModule))
                {
                    _propertySourceGenerator.Generate(property, sb);
                }
            }

            // Methods
            var methods = symbol.GetMembers().Where(m => m.Kind == SymbolKind.Method);
            foreach (IMethodSymbol method in methods)
            {
                if (HasTraceAttribute(method) || HasTraceAttribute(method.ContainingType) || HasTraceAttribute(method.ContainingNamespace) || HasTraceAttribute(method.ContainingAssembly) || HasTraceAttribute(method.ContainingModule))
                {
                    _methodSourceGenerator.Generate(method, sb);
                }
            }

            return sb.ToString();
        }
    }
}
