using AutoTrace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    public partial class Calculator
    {
        [AutoTrace]
        public int Add(int arg1, int arg2)
        {
            return arg1 + arg2;
        }

        //[AutoTrace]
        public int Divide(int arg1, int arg2)
        {
            return Add(arg1, arg1) / arg2;
        }

        //[AutoTrace]
        public int Multiply(int arg1, int arg2)
        {
            return arg1 * arg2;
        }
    }
}
