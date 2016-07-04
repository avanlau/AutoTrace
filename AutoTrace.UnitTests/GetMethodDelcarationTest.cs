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
    public class GetMethodDelcarationTest : TestBase
    {
        public GetMethodDelcarationTest(TextCodeFixture fixture) : base(fixture)
        {
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
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueAndParameters", "AutoTrace.TraceEnterMethod($\"Enter: public void TestNamespace.TestClass.MethodWithoutReturnValueAndParameters()\"")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueWithoutParameters", "AutoTrace.TraceEnterMethod($\"Enter: public Int32 TestNamespace.TestClass.MethodWithReturnValueWithoutParameters()\"")]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueWithParameters", "AutoTrace.TraceEnterMethod($\"Enter: public void TestNamespace.TestClass.MethodWithoutReturnValueWithParameters(Int32 a, String b)\"")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueAndParameters", "AutoTrace.TraceEnterMethod($\"Enter: public String TestNamespace.TestClass.MethodWithReturnValueAndParameters(Int32 a, String b)\"")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueAndParameters", "AutoTrace.TraceEnterMethod($\"Enter: public void TestNamespace.TestStruct.MethodWithoutReturnValueAndParameters()\"")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueWithoutParameters", "AutoTrace.TraceEnterMethod($\"Enter: public Int32 TestNamespace.TestStruct.MethodWithReturnValueWithoutParameters()\"")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueWithParameters", "AutoTrace.TraceEnterMethod($\"Enter: public void TestNamespace.TestStruct.MethodWithoutReturnValueWithParameters(Int32 a, String b)\"")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueAndParameters", "AutoTrace.TraceEnterMethod($\"Enter: public String TestNamespace.TestStruct.MethodWithReturnValueAndParameters(Int32 a, String b)\"")]
        public void TraceEnterMethodTests(TypeKind typeKind, string methodName, string expected)
        {
            // Arange
            IMethodSymbol methodSymbol = GetMetodSymbol(typeKind, methodName);

            // Act
            var actual = methodSymbol.TraceEnterMethod();

            // Assert;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueAndParameters", "AutoTrace.TraceLeaveMethod($\"Leave: public void TestNamespace.TestClass.MethodWithoutReturnValueAndParameters()\"")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueWithoutParameters", "AutoTrace.TraceLeaveMethod($\"Leave: public Int32 TestNamespace.TestClass.MethodWithReturnValueWithoutParameters()\"")]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueWithParameters", "AutoTrace.TraceLeaveMethod($\"Leave: public void TestNamespace.TestClass.MethodWithoutReturnValueWithParameters(Int32 a, String b)\"")]
        [InlineData(TypeKind.Class, "MethodWithReturnValueAndParameters", "AutoTrace.TraceLeaveMethod($\"Leave: public String TestNamespace.TestClass.MethodWithReturnValueAndParameters(Int32 a, String b)\"")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueAndParameters", "AutoTrace.TraceLeaveMethod($\"Leave: public void TestNamespace.TestStruct.MethodWithoutReturnValueAndParameters()\"")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueWithoutParameters", "AutoTrace.TraceLeaveMethod($\"Leave: public Int32 TestNamespace.TestStruct.MethodWithReturnValueWithoutParameters()\"")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueWithParameters", "AutoTrace.TraceLeaveMethod($\"Leave: public void TestNamespace.TestStruct.MethodWithoutReturnValueWithParameters(Int32 a, String b)\"")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueAndParameters", "AutoTrace.TraceLeaveMethod($\"Leave: public String TestNamespace.TestStruct.MethodWithReturnValueAndParameters(Int32 a, String b)\"")]
        public void TraceLeaveMethodTests(TypeKind typeKind, string methodName, string expected)
        {
            // Arange
            IMethodSymbol methodSymbol = GetMetodSymbol(typeKind, methodName);

            // Act
            var actual = methodSymbol.TraceLeaveMethod();

            // Assert;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueWithParameters", "AutoTrace.TraceParameter($\"{nameof(a)} = {a}\");")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueWithParameters", "AutoTrace.TraceParameter($\"{nameof(a)} = {a}\");")]
        public void TraceParameterTests(TypeKind typeKind, string methodName, string expected)
        {
            // Arange
            IMethodSymbol methodSymbol = GetMetodSymbol(typeKind, methodName);
            IParameterSymbol parameterSymbol = methodSymbol.Parameters.Where(p => p.Name.Equals("a")).First();

            // Act
            var actual = parameterSymbol.TraceParameter();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(TypeKind.Class, "MethodWithReturnValueWithoutParameters", "AutoTrace.TraceParameter($\"ReturValue = {result}\");")]
        [InlineData(TypeKind.Struct, "MethodWithReturnValueWithoutParameters", "AutoTrace.TraceParameter($\"ReturValue = {result}\");")]
        [InlineData(TypeKind.Class, "MethodWithoutReturnValueAndParameters", "")]
        [InlineData(TypeKind.Struct, "MethodWithoutReturnValueAndParameters", "")]
        public void TraceReturnValueTests(TypeKind typeKind, string methodName, string expected)
        {
            // Arange
            IMethodSymbol methodSymbol = GetMetodSymbol(typeKind, methodName);

            // Act
            var actual = methodSymbol.TraceReturnValue();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
