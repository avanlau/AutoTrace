using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrace.UnitTests
{
    public class TestBase
    {
        public INamedTypeSymbol testClassSymbol;
        public INamedTypeSymbol testStructSymbol;
        public string source;
        public const string classCategory = "Class";
        public const string structCategory = "Struct";

        [TestInitialize]
        public void Init()
        {
            source = File.ReadAllText("TestCode.cs");
            var tree = CSharpSyntaxTree.ParseText(source);

            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var compilation = CSharpCompilation.Create("MyCompilation", syntaxTrees: new[] { tree }, references: new[] { mscorlib });
            testClassSymbol = compilation.GetTypeByMetadataName("TestNamespace.TestClass");
            testStructSymbol = compilation.GetTypeByMetadataName("TestNamespace.TestStruct");
        }
    }
}
