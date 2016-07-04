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
    public class TextCodeFixture : IDisposable
    {
        public TextCodeFixture()
        {
            Source = File.ReadAllText("TestCode.cs");
            var tree = CSharpSyntaxTree.ParseText(Source);

            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var compilation = CSharpCompilation.Create("TestCodeCompilation", syntaxTrees: new[] { tree }, references: new[] { mscorlib });
            TestClassSymbol = compilation.GetTypeByMetadataName("TestNamespace.TestClass");
            TestStructSymbol = compilation.GetTypeByMetadataName("TestNamespace.TestStruct");

            SourceResult = File.ReadAllText("TestCodeResult.cs");
            var treeResult = CSharpSyntaxTree.ParseText(SourceResult);

            var compilationResult = CSharpCompilation.Create("TestCodeResultCompilation", syntaxTrees: new[] { treeResult }, references: new[] { mscorlib });
            TestClassSymbolResult = compilationResult.GetTypeByMetadataName("TestNamespace.Result.TestClass");
            TestStructSymbolResult = compilationResult.GetTypeByMetadataName("TestNamespace.Result.TestStruct");
        }

        public void Dispose()
        {

        }

        public string Source { get; set; }
        public INamedTypeSymbol TestClassSymbol { get; private set; }
        public INamedTypeSymbol TestStructSymbol { get; private set; }

        public string SourceResult { get; set; }
        public INamedTypeSymbol TestClassSymbolResult { get; private set; }
        public INamedTypeSymbol TestStructSymbolResult { get; private set; }
    }

    [CollectionDefinition("TextCode")]
    public class DatabaseCollection : ICollectionFixture<TextCodeFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
