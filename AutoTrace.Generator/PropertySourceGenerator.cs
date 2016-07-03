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

        }
    }
}
