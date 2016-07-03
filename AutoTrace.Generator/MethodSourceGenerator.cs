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
            //            string r = $@"
            //        replace {symbol.DeclaredAccessibility.ToString().ToLower()} {symbol.ReturnType.Name} {symbol.Name}({symbol.GetMethodParameters()})
            //        {{
            //            MDEVTrace.TraceEnter($""Enter: {symbol.GetMethodDelcaration()}"");
            //{GetParameterTrace(method)}

            //            try
            //            {{
            //                var result = original({GetParametersCall(method)});
            //                MDEVTrace.TraceParameter($""ReturValue = {{result}}"");
            //                MDEVTrace.TraceLeave($""Leave: {symbol.GetMethodDelcaration()}"");
            //                return result;
            //            }}
            //            catch (Exception e)
            //            {{
            //                MDEVTrace.TraceError($""Exception: {{e.GetType().Name}}{{Environment.NewLine}}{{e.Message}}"");
            //                throw;
            //            }}

            //        }}
            //";
            sb.AppendLine("using System;");
            sb.AppendLine("using AutoTrace;");
            sb.AppendLine(symbol.GetMethodDeclaration());
            sb.AppendLine(OpenCurlyBraces());
            sb.AppendLine(symbol.TraceEnterMethod());
            foreach (IParameterSymbol parameter in symbol.Parameters)
            {
                sb.AppendLine(parameter.TraceParameter());
            }
            sb.AppendLine("try");
            sb.AppendLine(OpenCurlyBraces());
            sb.AppendLine(symbol.CallOriginalMethod());
            sb.AppendLine(symbol.TraceReturnValue());
            sb.AppendLine(symbol.TraceLeaveMethod());
            sb.AppendLine(ReturnValue());
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
        private string ReturnValue() => "return result;";
    }

    public static class MethodExtensions
    {
        public static string GetMethodParameters(this IMethodSymbol method)
        {
            StringBuilder sb = new StringBuilder();

            foreach (IParameterSymbol parameter in method.Parameters)
            {
                sb.Append(parameter.Type.Name);
                sb.Append(" ");
                sb.Append(parameter.Name);
                sb.Append(", ");
            }

            if (sb.Length > 1)
                sb.Remove(sb.Length - 2, 2); // delete last , 

            return sb.ToString();
        }

        public static string GetMethodArguments(this IMethodSymbol symbol)
        {
            StringBuilder sb = new StringBuilder();

            foreach (IParameterSymbol parameter in symbol.Parameters)
            {
                sb.Append(parameter.Name);
                sb.Append(", ");
            }

            if (sb.Length > 1)
                sb.Remove(sb.Length - 2, 2); // delete last , 

            return sb.ToString();
        }

        public static string GetMethodDeclaration(this IMethodSymbol method) => $"{method.DeclaredAccessibility.ToString().ToLower()} {method.GetMethodReturnType()} {method.Name}({method.GetMethodParameters()})";
        public static string GetMethodDeclarationWithNamespace(this IMethodSymbol method) => $"{method.DeclaredAccessibility.ToString().ToLower()} {method.GetMethodReturnType()} {method.ContainingNamespace.ToString()}.{method.ContainingType.Name}.{method.Name}({method.GetMethodParameters()})";
        public static string GetMethodReturnType(this IMethodSymbol symbol) => symbol.ReturnsVoid ? "void" : symbol.ReturnType.Name;
        public static string GetMethodReturnValue(this IMethodSymbol symbol) => symbol.ReturnsVoid ? string.Empty : "var result = ";
        public static string CallOriginalMethod(this IMethodSymbol symbol) => $@"{symbol.GetMethodReturnValue()}original({symbol.GetMethodArguments()});";
        public static string TraceEnterMethod(this IMethodSymbol symbol) => $@"AutoTrace.TraceEnterMethod($""Enter: {symbol.GetMethodDeclarationWithNamespace()}""";
        public static string TraceLeaveMethod(this IMethodSymbol symbol) => $@"AutoTrace.TraceLeaveMethod($""Leave: {symbol.GetMethodDeclarationWithNamespace()}""";
        public static string TraceParameter(this IParameterSymbol parameter) => $@"AutoTrace.TraceParameter($""{{nameof({parameter.Name})}} = {{{parameter.Name}}}"");";
        public static string TraceReturnValue(this IMethodSymbol symbol) => symbol.ReturnsVoid ? string.Empty : $@"MDEVTrace.TraceParameter($""ReturValue = {{result}}"");";
    }
}