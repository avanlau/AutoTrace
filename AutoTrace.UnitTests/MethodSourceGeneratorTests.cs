using AutoTrace.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoTrace.UnitTests
{
    [Collection("TextCode")]
    public class MethodSourceGeneratorTests : TestBase
    {
        public MethodSourceGeneratorTests(TextCodeFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueAndParameters")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueWithoutParameters")]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueWithParameters")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueAndParameters")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueAndParameters")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueWithoutParameters")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueWithParameters")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueAndParameters")]
        public void The_Generate_Method_SOULD_Return_SourceCode_With_Trace_Functionality(TypeKind typeKind, string methodName)
        {
            // Arrange
            string expected = Normalize(GetSource(GetMetodSymbol(typeKind, methodName, true), methodName));
            MethodSourceGenerator generator = new MethodSourceGenerator();
            StringBuilder sb = new StringBuilder();
            IMethodSymbol method = GetMetodSymbol(typeKind, methodName);

            // Act
            generator.Generate(method, sb);
            string code = sb.ToString();
            string actual = Normalize(code);

            // Assert
            Assert.Equal(expected, actual);
        }

        private string GetSource(IMethodSymbol symbol, string methodName)
        {
            var syntaxTree = symbol.DeclaringSyntaxReferences.First().SyntaxTree;
            var root = syntaxTree.GetRoot();
            var method = root.DescendantNodes()
                .OfType<TypeDeclarationSyntax>()
                .Where(ns => ns.Identifier.ValueText.Equals(symbol.ContainingType.Name)).First().DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(md => md.Identifier.ValueText.Equals(methodName))
                .FirstOrDefault();

            return method.ToString();
        }

        private static string Normalize(string code)
        {
            var source = SyntaxFactory.ParseSyntaxTree(code, encoding: Encoding.UTF8);

            var actual = source.GetRoot().NormalizeWhitespace().ToFullString();
            return actual;
        }
    }
}