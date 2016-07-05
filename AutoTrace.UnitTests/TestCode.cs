using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNamespace
{
    public partial class TestClass
    {
        public TestClass()
        {

        }

        public TestClass(int a, string b)
        {

        }

        public int AutoProperty { get; set; }
        public int AutoPrivateSetProperty { get; private set; }

        private string myProperty;
        public string MyProperty
        {
            get { return myProperty; }
            set { myProperty = value; }
        }

        public void MethodWithoutReturnValueAndParameters()
        { }

        public void MethodWithoutReturnValueWithParameters(int a, string b)
        { }

        public string MethodWithReturnValueAndParameters(int a, string b)
        {
            return $"a = {a}, b = {b}";
        }

        public int MethodWithReturnValueWithoutParameters()
        {
            return 1;
        }
    }

    public partial struct TestStruct
    {
        public TestStruct(int a, string b)
        {
            AutoProperty = 1;
            myProperty = "test";
        }

        public int AutoProperty { get; set; }

        private string myProperty;
        public string MyProperty
        {
            get { return myProperty; }
            set { myProperty = value; }
        }

        public void MethodWithoutReturnValueAndParameters()
        { }

        public void MethodWithoutReturnValueWithParameters(int a, string b)
        { }

        public string MethodWithReturnValueAndParameters(int a, string b)
        {
            return $"a = {a}, b = {b}";
        }

        public int MethodWithReturnValueWithoutParameters()
        {
            return 1;
        }
    }

}