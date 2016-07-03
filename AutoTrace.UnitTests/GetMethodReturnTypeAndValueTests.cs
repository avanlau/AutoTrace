using AutoTrace.Generator;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoTrace.UnitTests
{
    [Collection("TextCode")]
    public class GetMethodReturnTypeAndValueTests : TestBase
    {
        public GetMethodReturnTypeAndValueTests(TextCodeFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueAndParameters", "void")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueWithoutParameters", "Int32")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueAndParameters", "void")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueWithoutParameters", "Int32")]
        public void GetMethodReturnTypeTests(TypeKind typeKind, string methodName, string expected)
        {
            // Arange
            IMethodSymbol methodSymbol = GetMetodSymbol(TypeKind.Class, methodName);

            // Act
            var actual = methodSymbol.GetMethodReturnType();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueAndParameters", "original();")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueWithoutParameters", "var result = original();")]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueWithParameters", "original(a, b);")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueAndParameters", "var result = original(a, b);")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueAndParameters", "original();")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueWithoutParameters", "var result = original();")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueWithParameters", "original(a, b);")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueAndParameters", "var result = original(a, b);")]
        public void CallOriginalMethodTests(TypeKind typeKind, string methodName, string expected)
        {
            // Arange
            IMethodSymbol methodSymbol = GetMetodSymbol(TypeKind.Class, methodName);

            // Act
            var actual = methodSymbol.CallOriginalMethod();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
