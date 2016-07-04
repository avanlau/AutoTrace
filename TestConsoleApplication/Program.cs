using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int arg1 = 10;
            int arg2 = 2;
            Calculator calc = new Calculator();
            var result = calc.Divide(calc.Multiply(arg1,arg1), arg2);
            Console.WriteLine($"{arg1} devided by {arg2} is {result}");
        }
    }
}
