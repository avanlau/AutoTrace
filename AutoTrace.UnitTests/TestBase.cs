using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrace.UnitTests
{
    public enum TypeKind
    {
        Class,
        Struct
    }

    public class TestBase
    {
        public TextCodeFixture fixture { get; private set; }

        public TestBase(TextCodeFixture fixture)
        {
            this.fixture = fixture;
        }

        public IMethodSymbol GetMetodSymbol(TypeKind typeKind, string methodName, bool result = false)
        {
            INamedTypeSymbol symbol;
            if (result)
            {
                switch (typeKind)
                {
                    case (TypeKind.Class):
                        symbol = fixture.TestClassSymbolResult;
                        break;
                    case (TypeKind.Struct):
                        symbol = fixture.TestStructSymbolResult;
                        break;
                    default:
                        throw new NullReferenceException();
                }
            }
            else
            {
                switch (typeKind)
                {
                    case (TypeKind.Class):
                        symbol = fixture.TestClassSymbol;
                        break;
                    case (TypeKind.Struct):
                        symbol = fixture.TestStructSymbol;
                        break;
                    default:
                        throw new NullReferenceException();
                }
            }

            var methodSymbol = (IMethodSymbol)symbol.GetMembers().Where(m => m.Name.Equals(methodName)).First();

            return methodSymbol;
        }
    }
}
