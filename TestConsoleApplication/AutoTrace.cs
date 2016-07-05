using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrace
{
    public partial class AutoTraceAttribute : Attribute
    {
    }
}

namespace TestConsoleApplication
{
    public static class AutoTrace
    {
        private static string indent = "";

        public static void TraceEnterMethod(string message)
        {
            TraceInformation(message);
            TraceInformation("{");
            IncreseIndent();
        }

        public static void TraceParameter(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{System.DateTime.Now.ToLongTimeString()} : {indent}{message}");
            Console.ForegroundColor = color;
        }

        public static void TraceLeaveMethod(string message)
        {
            DecreseIndent();
            TraceInformation("}");
            TraceInformation(message);
            TraceInformation("");
        }

        private static void IncreseIndent()
        {
            indent += "  ";
        }

        private static void DecreseIndent()
        {
            indent = indent.Substring(0, indent.Length - 2);
        }

        public static void TraceInformation(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{System.DateTime.Now.ToLongTimeString()} : {indent}{message}");
            Console.ForegroundColor = color;
        }

        public static void TraceException(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{indent}{message}");
            Console.ForegroundColor = color;
        }
    }
}
