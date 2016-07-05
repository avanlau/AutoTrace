using System;
using System.Text;
using Microsoft.CodeAnalysis;

namespace AutoTrace.Generator
{
    public interface IMethodSourceGenerator
    {
        void Generate(IMethodSymbol symbol, StringBuilder sb);
    }

    public class MethodSourceGenerator : IMethodSourceGenerator
    {
        public void Generate(IMethodSymbol symbol, StringBuilder sb)
        {
            if (symbol.MethodKind != MethodKind.Ordinary)
                return;

            sb.Append($"replace ");
            sb.AppendLine(symbol.GetMethodDeclaration());
            sb.AppendLine(OpenCurlyBraces());
            sb.AppendLine("try");
            sb.AppendLine(OpenCurlyBraces());
            sb.AppendLine(symbol.TraceEnterMethod());
            foreach (IParameterSymbol parameter in symbol.Parameters)
            {
                sb.AppendLine(parameter.TraceParameter());
            }
            sb.AppendLine(symbol.CallOriginalMethod());
            sb.AppendLine(symbol.TraceReturnValue());
            sb.AppendLine(symbol.TraceLeaveMethod());
            sb.AppendLine(symbol.ReturnValue());
            sb.AppendLine(CloseCurlyBraces());
            sb.AppendLine("catch (Exception e)");
            sb.AppendLine(OpenCurlyBraces());
            sb.AppendLine(TraceException());
            sb.AppendLine("throw;");
            sb.AppendLine(CloseCurlyBraces());
            sb.AppendLine(CloseCurlyBraces());
            sb.AppendLine();
        }

        private string TraceException() => $@"AutoTrace.TraceException($""Exception: {{e.GetType().Name}}{{Environment.NewLine}}{{e.Message}}"");";
        private string OpenCurlyBraces() => $@"{{";
        private string CloseCurlyBraces() => $@"}}";
    }

    public static class MethodExtensions
    {
        public static string GetMethodParameters(this IMethodSymbol method)
        {
            StringBuilder sb = new StringBuilder();

            foreach (IParameterSymbol parameter in method.Parameters)
            {
                AddParameterName(sb, parameter);
                AddParameterValue(sb, parameter);
            }

            DeleteTrailingCommaAndWhitespace(sb);

            return sb.ToString();
        }

        public static string GetMethodArguments(this IMethodSymbol symbol)
        {
            StringBuilder sb = new StringBuilder();

            foreach (IParameterSymbol parameter in symbol.Parameters)
            {
                AddParameterValue(sb, parameter);
            }

            DeleteTrailingCommaAndWhitespace(sb);

            return sb.ToString();
        }
        public static string GetConstructorDeclaration(this IMethodSymbol method) => $"{method.DeclaredAccessibility.ToString().ToLower()} {method.ContainingType.Name}({method.GetMethodParameters()})";
        public static string GetMethodDeclaration(this IMethodSymbol method) => $"{method.DeclaredAccessibility.ToString().ToLower()} {method.GetMethodReturnType()} {method.Name}({method.GetMethodParameters()})";
        public static string GetMethodDeclarationWithNamespace(this IMethodSymbol method) => $"{method.DeclaredAccessibility.ToString().ToLower()} {method.GetMethodReturnType()} {method.ContainingNamespace.ToString()}.{method.ContainingType.Name}.{method.Name}({method.GetMethodParameters()})";
        public static string GetMethodReturnType(this IMethodSymbol symbol) => symbol.ReturnsVoid ? "void" : symbol.ReturnType.Name;
        public static string GetMethodReturnValue(this IMethodSymbol symbol) => symbol.ReturnsVoid ? string.Empty : "var result = ";
        public static string CallOriginalMethod(this IMethodSymbol symbol) => $@"{symbol.GetMethodReturnValue()}original({symbol.GetMethodArguments()});";
        public static string TraceEnterMethod(this IMethodSymbol symbol) => $@"AutoTrace.TraceEnterMethod($""Enter: {symbol.GetMethodDeclarationWithNamespace()}"");";
        public static string TraceLeaveMethod(this IMethodSymbol symbol) => $@"AutoTrace.TraceLeaveMethod($""Leave: {symbol.GetMethodDeclarationWithNamespace()}"");";
        public static string TraceParameter(this IParameterSymbol parameter) => $@"AutoTrace.TraceParameter($""{{nameof({parameter.Name})}} = {{{parameter.Name}}}"");";
        public static string TraceReturnValue(this IMethodSymbol symbol) => symbol.ReturnsVoid ? string.Empty : $@"AutoTrace.TraceParameter($""ReturValue = {{result}}"");";
        public static string ReturnValue(this IMethodSymbol symbol) => symbol.ReturnsVoid ? string.Empty : "return result;";

        private static void AddParameterName(StringBuilder sb, IParameterSymbol parameter)
        {
            sb.Append(parameter.Type.Name);
            sb.Append(" ");
        }

        private static void AddParameterValue(StringBuilder sb, IParameterSymbol parameter)
        {
            sb.Append(parameter.Name);
            sb.Append(", ");
        }

        private static void DeleteTrailingCommaAndWhitespace(StringBuilder sb)
        {
            if (sb.Length > 1)
                sb.Remove(sb.Length - 2, 2); // delete last , 
        }
    }
}