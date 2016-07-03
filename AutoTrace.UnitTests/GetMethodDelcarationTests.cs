using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;
using System.Linq;
using AutoTrace.Generator;

namespace AutoTrace.UnitTests
{
    [TestClass]
    public class GetMethodDelcarationTests : TestBase
    {
        public const string getMethodDeclarationCategory = "GetMethodDelcaration";
        public const string getMethodDeclarationCategoryWithNamespace = "GetMethodDelcarationWithNamespace";

        [TestMethod]
        [TestCategory(getMethodDeclarationCategory)]
        [TestCategory(classCategory)]
        public void GetMethodDelcaration_WITHOUT_ReturnVlaue_WITHOUT_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_class()
        {
            //Arange
            var expected = $@"public void MethodWithoutReturnValueAndParameters()";
            var methodName = "MethodWithoutReturnValueAndParameters";

            // Act
            string actual = RunGetMethodDeclarationTest(testClassSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategory)]
        [TestCategory(classCategory)]
        public void GetMethodDelcaration_WITHOUT_ReturnValue_WITH_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_class()
        {
            //Arange
            var expected = $@"public void MethodWithoutReturnValueWithParameters(Int32 a, String b)";
            var methodName = "MethodWithoutReturnValueWithParameters";

            // Act
            string actual = RunGetMethodDeclarationTest(testClassSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategory)]
        [TestCategory(classCategory)]
        public void GetMethodDelcaration_WITH_ReturnValue_WITH_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_class()
        {
            //Arange
            var expected = $@"public String MethodWithReturnValueAndParameters(Int32 a, String b)";
            var methodName = "MethodWithReturnValueAndParameters";

            // Act
            string actual = RunGetMethodDeclarationTest(testClassSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategory)]
        [TestCategory(classCategory)]
        public void GetMethodDelcaration_WITH_ReturnValue_WITHOUT_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_class()
        {
            // Arange
            var expected = $@"public Int32 MethodWithReturnValueWithoutParameters()";
            var methodName = "MethodWithReturnValueWithoutParameters";

            // Act
            string actual = RunGetMethodDeclarationTest(testClassSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [TestCategory(getMethodDeclarationCategory)]
        [TestCategory(structCategory)]
        public void GetMethodDelcaration_WITHOUT_ReturnVlaue_WITHOUT_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_struct()
        {
            //Arange
            var expected = $@"public void MethodWithoutReturnValueAndParameters()";
            var methodName = "MethodWithoutReturnValueAndParameters";

            // Act
            string actual = RunGetMethodDeclarationTest(testStructSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategory)]
        [TestCategory(structCategory)]
        public void GetMethodDelcaration_WITHOUT_ReturnValue_WITH_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_struct()
        {
            //Arange
            var expected = $@"public void MethodWithoutReturnValueWithParameters(Int32 a, String b)";
            var methodName = "MethodWithoutReturnValueWithParameters";

            // Act
            string actual = RunGetMethodDeclarationTest(testStructSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategory)]
        [TestCategory(structCategory)]
        public void GetMethodDelcaration_WITH_ReturnValue_WITH_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_struct()
        {
            //Arange
            var expected = $@"public String MethodWithReturnValueAndParameters(Int32 a, String b)";
            var methodName = "MethodWithReturnValueAndParameters";

            // Act
            string actual = RunGetMethodDeclarationTest(testStructSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategory)]
        [TestCategory(structCategory)]
        public void GetMethodDelcaration_WITH_ReturnValue_WITHOUT_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_struct()
        {
            // Arange
            var expected = $@"public Int32 MethodWithReturnValueWithoutParameters()";
            var methodName = "MethodWithReturnValueWithoutParameters";

            // Act
            string actual = RunGetMethodDeclarationTest(testStructSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategoryWithNamespace)]
        [TestCategory(classCategory)]
        public void GetMethodDelcarationWithNamespace_WITHOUT_ReturnVlaue_WITHOUT_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_class()
        {
            //Arange
            var expected = $@"public void TestNamespace.TestClass.MethodWithoutReturnValueAndParameters()";
            var methodName = "MethodWithoutReturnValueAndParameters";

            // Act
            string actual = RunGetMethodDeclarationTestWithNamespace(testClassSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategoryWithNamespace)]
        [TestCategory(classCategory)]
        public void GetMethodDelcarationWithNamespace_WITHOUT_ReturnValue_WITH_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_class()
        {
            //Arange
            var expected = $@"public void TestNamespace.TestClass.MethodWithoutReturnValueWithParameters(Int32 a, String b)";
            var methodName = "MethodWithoutReturnValueWithParameters";

            // Act
            string actual = RunGetMethodDeclarationTestWithNamespace(testClassSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategoryWithNamespace)]
        [TestCategory(classCategory)]
        public void GetMethodDelcarationWithNamespace_WITH_ReturnValue_WITH_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_class()
        {
            //Arange
            var expected = $@"public String TestNamespace.TestClass.MethodWithReturnValueAndParameters(Int32 a, String b)";
            var methodName = "MethodWithReturnValueAndParameters";

            // Act
            string actual = RunGetMethodDeclarationTestWithNamespace(testClassSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategoryWithNamespace)]
        [TestCategory(classCategory)]
        public void GetMethodDelcarationWithNamespace_WITH_ReturnValue_WITHOUT_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_class()
        {
            // Arange
            var expected = $@"public Int32 TestNamespace.TestClass.MethodWithReturnValueWithoutParameters()";
            var methodName = "MethodWithReturnValueWithoutParameters";

            // Act
            string actual = RunGetMethodDeclarationTestWithNamespace(testClassSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategoryWithNamespace)]
        [TestCategory(structCategory)]
        public void GetMethodDelcarationWithNamespace_WITHOUT_ReturnVlaue_WITHOUT_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_struct()
        {
            //Arange
            var expected = $@"public void TestNamespace.TestStruct.MethodWithoutReturnValueAndParameters()";
            var methodName = "MethodWithoutReturnValueAndParameters";

            // Act
            string actual = RunGetMethodDeclarationTestWithNamespace(testStructSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategoryWithNamespace)]
        [TestCategory(structCategory)]
        public void GetMethodDelcarationWithNamespace_WITHOUT_ReturnValue_WITH_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_struct()
        {
            //Arange
            var expected = $@"public void TestNamespace.TestStruct.MethodWithoutReturnValueWithParameters(Int32 a, String b)";
            var methodName = "MethodWithoutReturnValueWithParameters";

            // Act
            string actual = RunGetMethodDeclarationTestWithNamespace(testStructSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategoryWithNamespace)]
        [TestCategory(structCategory)]
        public void GetMethodDelcarationWithNamespace_WITH_ReturnValue_WITH_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_struct()
        {
            //Arange
            var expected = $@"public String TestNamespace.TestStruct.MethodWithReturnValueAndParameters(Int32 a, String b)";
            var methodName = "MethodWithReturnValueAndParameters";

            // Act
            string actual = RunGetMethodDeclarationTestWithNamespace(testStructSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory(getMethodDeclarationCategoryWithNamespace)]
        [TestCategory(structCategory)]
        public void GetMethodDelcarationWithNamespace_WITH_ReturnValue_WITHOUT_Parameters_SHOULD_return_the_first_line_of_the_class_in_the_SourceCode_WHEN_called_on_a_struct()
        {
            // Arange
            var expected = $@"public Int32 TestNamespace.TestStruct.MethodWithReturnValueWithoutParameters()";
            var methodName = "MethodWithReturnValueWithoutParameters";

            // Act
            string actual = RunGetMethodDeclarationTestWithNamespace(testStructSymbol, methodName);

            // Assert;
            Assert.AreEqual(expected, actual);
        }

        private string RunGetMethodDeclarationTest(INamedTypeSymbol symbol, string methodName)
        {
            var methodSymbol = (IMethodSymbol)symbol.GetMembers().Where(m => m.Name.Equals(methodName)).First();
            var actual = methodSymbol.GetMethodDeclaration();

            return actual;
        }

        private string RunGetMethodDeclarationTestWithNamespace(INamedTypeSymbol symbol, string methodName)
        {
            var methodSymbol = (IMethodSymbol)symbol.GetMembers().Where(m => m.Name.Equals(methodName)).First();
            var actual = methodSymbol.GetMethodDeclarationWithNamespace();

            return actual;
        }

    }
}
