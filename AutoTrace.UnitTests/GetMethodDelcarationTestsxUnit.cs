using AutoTrace.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoTrace.UnitTests
{
    public enum TypeKind
    {
        Class,
        Struct
    }

    [Collection("TextCode")]
    public class GetMethodDelcarationTestsxUnit
    {
        TextCodeFixture fixture;

        public GetMethodDelcarationTestsxUnit(TextCodeFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueAndParameters", "public void MethodWithoutReturnValueAndParameters()")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueWithoutParameters", "public Int32 MethodWithReturnValueWithoutParameters()")]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueWithParameters", "public void MethodWithoutReturnValueWithParameters(Int32 a, String b)")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueAndParameters", "public String MethodWithReturnValueAndParameters(Int32 a, String b)")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueAndParameters", "public void MethodWithoutReturnValueAndParameters()")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueWithoutParameters", "public Int32 MethodWithReturnValueWithoutParameters()")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueWithParameters", "public void MethodWithoutReturnValueWithParameters(Int32 a, String b)")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueAndParameters", "public String MethodWithReturnValueAndParameters(Int32 a, String b)")]
        public void GetMethodDeclarationTests(TypeKind typeKind, string methodName, string expected)
        {
            // Arange
            IMethodSymbol methodSymbol = GetMetodSymbol(typeKind, methodName);

            // Act
            var actual = methodSymbol.GetMethodDeclaration();

            // Assert;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueAndParameters", "public void TestNamespace.TestClass.MethodWithoutReturnValueAndParameters()")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueWithoutParameters", "public Int32 TestNamespace.TestClass.MethodWithReturnValueWithoutParameters()")]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueWithParameters", "public void TestNamespace.TestClass.MethodWithoutReturnValueWithParameters(Int32 a, String b)")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueAndParameters", "public String TestNamespace.TestClass.MethodWithReturnValueAndParameters(Int32 a, String b)")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueAndParameters", "public void TestNamespace.TestStruct.MethodWithoutReturnValueAndParameters()")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueWithoutParameters", "public Int32 TestNamespace.TestStruct.MethodWithReturnValueWithoutParameters()")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueWithParameters", "public void TestNamespace.TestStruct.MethodWithoutReturnValueWithParameters(Int32 a, String b)")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueAndParameters", "public String TestNamespace.TestStruct.MethodWithReturnValueAndParameters(Int32 a, String b)")]
        public void GetMethodDelcarationWithNamespace(TypeKind typeKind, string methodName, string expected)
        {
            // Arange
            IMethodSymbol methodSymbol = GetMetodSymbol(typeKind, methodName);

            // Act
            var actual = methodSymbol.GetMethodDeclarationWithNamespace();

            // Assert;
            Assert.Equal(expected, actual);
        }

        private IMethodSymbol GetMetodSymbol(TypeKind typeKind, string methodName)
        {
            INamedTypeSymbol symbol;
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

            var methodSymbol = (IMethodSymbol)symbol.GetMembers().Where(m => m.Name.Equals(methodName)).First();

            return methodSymbol;
        }
    }
}
