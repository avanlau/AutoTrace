using AutoTrace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication.BussinesLogic
{
    [AutoTrace]
    public partial class Calculator
    {
        //[AutoTrace]
        public int Arg1 { get; set; }

        private int arg2;
        //[AutoTrace]
        public int Arg2
        {
            get { return arg2; }
            set { arg2 = value; }
        }

        //[AutoTrace]
        public int DivideSavedValuesAndMultiplyBy(int multiplier)
        {
            return (Arg1 / Arg2) * multiplier;
        }

        //[AutoTrace]
        public int Add(int arg1, int arg2)
        {
            return arg1 + arg2;
        }

        //[AutoTrace]
        public int Divide(int arg1, int arg2)
        {
            return arg1 / arg2;
        }

        //[AutoTrace]
        public int Multiply(int arg1, int arg2)
        {
            return arg1 * arg2;
        }
    }
}
