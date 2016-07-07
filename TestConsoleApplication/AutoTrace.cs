using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrace
{
    public class AutoTraceAttribute : Attribute
    {
        public AutoTraceAttribute(Type traceType)
        {
        }
    }
}

namespace TestConsoleApplication
{
    public static class ConsoleIndent
    {
        static ConsoleIndent()
        {
            Indent = "";
        }

        public static string Indent { get; set; }
    }

    public static class ConsoleTracer
    {
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
            Console.WriteLine($"{GetLogTimeStamp()} : {ConsoleIndent.Indent}{message}");
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
            ConsoleIndent.Indent += "  ";
        }

        private static void DecreseIndent()
        {
            ConsoleIndent.Indent = ConsoleIndent.Indent.Substring(0, ConsoleIndent.Indent.Length - 2);
        }

        public static void TraceInformation(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{GetLogTimeStamp()} : {ConsoleIndent.Indent}{message}");
            Console.ForegroundColor = color;
        }

        public static void TraceException(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{ConsoleIndent.Indent}{message}");
            Console.ForegroundColor = color;
        }

        private static string GetLogTimeStamp()
        {
            return $"{System.DateTime.Now.ToShortDateString()} {System.DateTime.Now.ToLongTimeString()}";
        }
    }

    public static class ConsoleTracer2
    {
        public static void TraceEnterMethod(string message)
        {
            TraceInformation(message);
            TraceInformation("{");
            IncreseIndent();
        }

        public static void TraceParameter(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{GetLogTimeStamp()} : {ConsoleIndent.Indent}{message}");
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
            ConsoleIndent.Indent += "  ";
        }

        private static void DecreseIndent()
        {
            ConsoleIndent.Indent = ConsoleIndent.Indent.Substring(0, ConsoleIndent.Indent.Length - 2);
        }

        public static void TraceInformation(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{GetLogTimeStamp()} : {ConsoleIndent.Indent}{message}");
            Console.ForegroundColor = color;
        }

        public static void TraceException(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{ConsoleIndent.Indent}{message}");
            Console.ForegroundColor = color;
        }

        private static string GetLogTimeStamp()
        {
            return $"{System.DateTime.Now.ToShortDateString()} {System.DateTime.Now.ToLongTimeString()}";
        }
    }
}
