using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsoleApplication.BussinesLogic;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int arg1 = 10;
            int arg2 = 2;
            Calculator calc = new Calculator();
            calc.Arg1 = arg1;
            calc.Arg2 = arg2;
            var result = calc.DivideSavedValuesAndMultiplyBy(2);
            //var result = calc.Divide(arg1, arg2);
            Console.WriteLine($"{arg1} devided by {arg2} is {result}");
        }
    }
}
