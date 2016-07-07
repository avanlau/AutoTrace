using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoTrace;

namespace TestNamespace
{
    public partial class TestClass
    {
        // NOT SUPPORTED
        //replace public TestClass(int a, string b)
        //{
        //    Console.WriteLine("Changed Constructor");
        //}
        replace public void MethodWithoutReturnValueAndParameters()
        {
            try
            {
                AutoTrace.TraceEnterMethod($"Enter: public void TestNamespace.TestClass.MethodWithoutReturnValueAndParameters()");
                original();
                AutoTrace.TraceLeaveMethod($"Leave: public void TestNamespace.TestClass.MethodWithoutReturnValueAndParameters()");
            }
            catch (Exception e)
            {
                AutoTrace.TraceException($"Exception: {e.GetType().Name}{Environment.NewLine}{e.Message}");
                throw;
            }
        }

        replace public Int32 MethodWithReturnValueWithoutParameters()
        {
            try
            {
                AutoTrace.TraceEnterMethod($"Enter: public Int32 TestNamespace.TestClass.MethodWithReturnValueWithoutParameters()");
                var result = original();
                AutoTrace.TraceParameter($"ReturValue = {result}");
                AutoTrace.TraceLeaveMethod($"Leave: public Int32 TestNamespace.TestClass.MethodWithReturnValueWithoutParameters()");
                return result;
            }
            catch (Exception e)
            {
                AutoTrace.TraceException($"Exception: {e.GetType().Name}{Environment.NewLine}{e.Message}");
                throw;
            }
        }

        replace public void MethodWithoutReturnValueWithParameters(Int32 a, String b)
        {
            try
            {
                AutoTrace.TraceEnterMethod($"Enter: public void TestNamespace.TestClass.MethodWithoutReturnValueWithParameters(Int32 a, String b)");
                AutoTrace.TraceParameter($"{nameof(a)} = {a}");
                AutoTrace.TraceParameter($"{nameof(b)} = {b}");
                original(a, b);
                AutoTrace.TraceLeaveMethod($"Leave: public void TestNamespace.TestClass.MethodWithoutReturnValueWithParameters(Int32 a, String b)");
            }
            catch (Exception e)
            {
                AutoTrace.TraceException($"Exception: {e.GetType().Name}{Environment.NewLine}{e.Message}");
                throw;
            }
        }

        replace public String MethodWithReturnValueAndParameters(Int32 a, String b)
        {
            try
            {
                AutoTrace.TraceEnterMethod($"Enter: public String TestNamespace.TestClass.MethodWithReturnValueAndParameters(Int32 a, String b)");
                AutoTrace.TraceParameter($"{nameof(a)} = {a}");
                AutoTrace.TraceParameter($"{nameof(b)} = {b}");
                var result = original(a, b);
                AutoTrace.TraceParameter($"ReturValue = {result}");
                AutoTrace.TraceLeaveMethod($"Leave: public String TestNamespace.TestClass.MethodWithReturnValueAndParameters(Int32 a, String b)");
                return result;
            }
            catch (Exception e)
            {
                AutoTrace.TraceException($"Exception: {e.GetType().Name}{Environment.NewLine}{e.Message}");
                throw;
            }
        }
    }

    public partial struct TestStruct
    {
        replace public void MethodWithoutReturnValueAndParameters()
        {
            try
            {
                AutoTrace.TraceEnterMethod($"Enter: public void TestNamespace.TestStruct.MethodWithoutReturnValueAndParameters()");
                original();
                AutoTrace.TraceLeaveMethod($"Leave: public void TestNamespace.TestStruct.MethodWithoutReturnValueAndParameters()");
            }
            catch (Exception e)
            {
                AutoTrace.TraceException($"Exception: {e.GetType().Name}{Environment.NewLine}{e.Message}");
                throw;
            }
        }

        replace public Int32 MethodWithReturnValueWithoutParameters()
        {
            try
            {
                AutoTrace.TraceEnterMethod($"Enter: public Int32 TestNamespace.TestStruct.MethodWithReturnValueWithoutParameters()");
                var result = original();
                AutoTrace.TraceParameter($"ReturValue = {result}");
                AutoTrace.TraceLeaveMethod($"Leave: public Int32 TestNamespace.TestStruct.MethodWithReturnValueWithoutParameters()");
                return result;
            }
            catch (Exception e)
            {
                AutoTrace.TraceException($"Exception: {e.GetType().Name}{Environment.NewLine}{e.Message}");
                throw;
            }
        }

        replace public void MethodWithoutReturnValueWithParameters(Int32 a, String b)
        {
            try
            {
                AutoTrace.TraceEnterMethod($"Enter: public void TestNamespace.TestStruct.MethodWithoutReturnValueWithParameters(Int32 a, String b)");
                AutoTrace.TraceParameter($"{nameof(a)} = {a}");
                AutoTrace.TraceParameter($"{nameof(b)} = {b}");
                original(a, b);
                AutoTrace.TraceLeaveMethod($"Leave: public void TestNamespace.TestStruct.MethodWithoutReturnValueWithParameters(Int32 a, String b)");
            }
            catch (Exception e)
            {
                AutoTrace.TraceException($"Exception: {e.GetType().Name}{Environment.NewLine}{e.Message}");
                throw;
            }
        }

        replace public String MethodWithReturnValueAndParameters(Int32 a, String b)
        {
            try
            {
                AutoTrace.TraceEnterMethod($"Enter: public String TestNamespace.TestStruct.MethodWithReturnValueAndParameters(Int32 a, String b)");
                AutoTrace.TraceParameter($"{nameof(a)} = {a}");
                AutoTrace.TraceParameter($"{nameof(b)} = {b}");
                var result = original(a, b);
                AutoTrace.TraceParameter($"ReturValue = {result}");
                AutoTrace.TraceLeaveMethod($"Leave: public String TestNamespace.TestStruct.MethodWithReturnValueAndParameters(Int32 a, String b)");
                return result;
            }
            catch (Exception e)
            {
                AutoTrace.TraceException($"Exception: {e.GetType().Name}{Environment.NewLine}{e.Message}");
                throw;
            }
        }
    }

    public static class AutoTrace
    {
        public static void TraceEnterMethod(string message)
        {
        }

        public static void TraceLeaveMethod(string message)
        {
        }

        public static void TraceParameter(string message)
        {
        }

        public static void TraceException(string message)
        {
        }
    }
}