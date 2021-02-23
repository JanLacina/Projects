using System;
using System.Collections.Generic;

public class UniqueNumbers
{    public static void Main(string[] args)
    {
        MyClass myClass = new MyClass();

        Console.WriteLine(myClass.Method(5));
    }
    public class MyClass
    {
        public int a;
        public MyClass()
        {
            a = 5;
        }
        public int Method(int b)
        {
            return a + b;
        }
    }

}