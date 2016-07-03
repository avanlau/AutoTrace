using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrace.Generator
{
    /// <summary>
    /// Walk the symbol graph boilerplate code
    /// </summary>
    public abstract class TraceSymbolVisitorBase : SymbolVisitor
    {
        protected readonly SourceGeneratorContext _context;
        protected readonly Compilation _comp;
        protected readonly INamedTypeSymbol _autoTraceAttributeSymbol;

        public TraceSymbolVisitorBase(SourceGeneratorContext context)
        {
            _context = context;
            _comp = context.Compilation;
            _autoTraceAttributeSymbol = _comp.GetTypeByMetadataName("AutoTrace.AutoTraceAttribute");
        }

        public override void VisitModule(IModuleSymbol symbol)
        {
            VisitNamespace(symbol.GlobalNamespace);
        }

        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            foreach (var n in symbol.GetNamespaceMembers())
            {
                VisitNamespace(n);
            }

            foreach (var t in symbol.GetTypeMembers())
            {
                VisitNamedType(t);
            }
        }

        public override void VisitNamedType(INamedTypeSymbol type)
        {
            foreach (var t in type.GetTypeMembers())
            {
                VisitNamedType(t);
            }

            ProcessType(type);
        }

        public abstract void ProcessType(INamedTypeSymbol symbol);

        public bool HasTraceAttribute(ISymbol symbol)
        {
            return symbol.GetAttributes().Where(a => a.AttributeClass == _autoTraceAttributeSymbol).Count() != 0 ? true : false;
        }
    }
}
