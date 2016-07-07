using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrace.Generator
{
    public interface IPropertySourceGenerator
    {
        void Generate(IPropertySymbol symbol, StringBuilder sb);
    }

    public class PropertySourceGenerator : IPropertySourceGenerator
    {
        public void Generate(IPropertySymbol symbol, StringBuilder sb)
        {
            sb.AppendLine($"replace {symbol.GetPropertyDeclaration()}");
            sb.AppendLine(OpenCurlyBraces());

            // Get
            if (symbol.GetMethod != null)
            {
                sb.AppendLine(symbol.GetMethod.GetPropertyGetMethodDeclaration());
                sb.AppendLine(OpenCurlyBraces());
                sb.AppendLine("try");
                sb.AppendLine(OpenCurlyBraces());
                sb.AppendLine(symbol.TraceEnterPropertyGet());
                sb.AppendLine("var result = original;");
                sb.AppendLine(symbol.TraceReturnValueProperty());
                sb.AppendLine(symbol.TraceLeavePropertyGet());
                sb.AppendLine("return result;");
                sb.AppendLine(CloseCurlyBraces());
                sb.AppendLine("catch (Exception e)");
                sb.AppendLine(OpenCurlyBraces());
                sb.AppendLine(symbol.TraceException());
                sb.AppendLine("throw;");
                sb.AppendLine(CloseCurlyBraces());
                sb.AppendLine(CloseCurlyBraces());
            }

            // Set
            if (symbol.SetMethod != null)
            {
                sb.AppendLine(symbol.SetMethod.GetPropertySetMethodDeclaration());
                sb.AppendLine(OpenCurlyBraces());
                sb.AppendLine("try");
                sb.AppendLine(OpenCurlyBraces());
                sb.AppendLine(symbol.TraceEnterPropertySet());
                sb.AppendLine(symbol.TraceNewValueProperty());
                sb.AppendLine("original = value;");
                sb.AppendLine(symbol.TraceLeavePropertySet());
                sb.AppendLine(CloseCurlyBraces());
                sb.AppendLine("catch (Exception e)");
                sb.AppendLine(OpenCurlyBraces());
                sb.AppendLine(symbol.TraceException());
                sb.AppendLine("throw;");
                sb.AppendLine(CloseCurlyBraces());
                sb.AppendLine(CloseCurlyBraces());
            }

            sb.AppendLine(CloseCurlyBraces());
        }

        private string OpenCurlyBraces() => $@"{{";
        private string CloseCurlyBraces() => $@"}}";
    }

    public static class PropertyExtensions
    {
        public static string TraceException(this IPropertySymbol symbol) => $@"{symbol.GetTraceType()}.TraceException($""Exception: {{e.GetType().Name}}{{Environment.NewLine}}{{e.Message}}"");";
        public static string TraceReturnValueProperty(this IPropertySymbol symbol) => $@"{symbol.GetTraceType()}.TraceParameter($""ReturValue = {{result}}"");";
        public static string TraceNewValueProperty(this IPropertySymbol symbol) => $@"{symbol.GetTraceType()}.TraceParameter($""NewValue = {{value}}"");";


        public static string GetPropertyDeclaration(this IPropertySymbol symbol) => $@"{symbol.DeclaredAccessibility.ToString().ToLower()} {symbol.Type.Name} {symbol.Name}";
        public static string GetPropertyGetMethodDeclaration(this IMethodSymbol symbol) => $"{symbol.GetAccessibility()} get";
        public static string GetPropertySetMethodDeclaration(this IMethodSymbol symbol) => $"{symbol.GetAccessibility()} set";
        public static string GetAccessibility(this IMethodSymbol symbol) => symbol.DeclaredAccessibility.ToString().ToLower().Equals("public") ? string.Empty : symbol.DeclaredAccessibility.ToString().ToLower();
        public static string TraceEnterPropertyGet(this IPropertySymbol symbol) => $@"{symbol.GetTraceType()}.TraceEnterMethod($""Enter: get_{symbol.Name}"");";
        public static string TraceLeavePropertyGet(this IPropertySymbol symbol) => $@"{symbol.GetTraceType()}.TraceLeaveMethod($""Leave: get_{symbol.Name}"");";

        public static string TraceEnterPropertySet(this IPropertySymbol symbol) => $@"{symbol.GetTraceType()}.TraceEnterMethod($""Enter: set_{symbol.Name}"");";
        public static string TraceLeavePropertySet(this IPropertySymbol symbol) => $@"{symbol.GetTraceType()}.TraceLeaveMethod($""Leave: set_{symbol.Name}"");";

        public static string GetTraceType(this IPropertySymbol symbol)
        {
            if (symbol.GetAttributes().FirstOrDefault() != null)
                return ((symbol.GetAttributes()[0].ConstructorArguments).FirstOrDefault()).Value.ToString();

            if(symbol.ContainingType.GetAttributes().FirstOrDefault() != null)
                return ((symbol.ContainingType.GetAttributes()[0].ConstructorArguments).FirstOrDefault()).Value.ToString();

            throw new ArgumentException("Can't find trace type.");
        }
    }
}
