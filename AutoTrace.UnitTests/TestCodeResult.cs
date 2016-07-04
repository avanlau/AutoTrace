using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoTrace;

namespace TestNamespace.Result
{
    public partial class TestClass
    {
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