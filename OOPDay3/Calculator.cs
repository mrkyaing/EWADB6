using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPDay3
{
    public class Calculator
    {
        public void Add(int n1, int n2)
        {
            Console.WriteLine("The result:"+n1 + n2);
        }

        public void Add(double n1, double n2)
        {
            Console.WriteLine("The result:"+n1 + n2);
        }

        public void Add(int n1, int n2,int n3)
        {
            Console.WriteLine("The result:"+n1 + n2+n3);
        }
    }
}