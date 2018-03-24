using GK.Reflection;
using System;

namespace GK.Cocos.ConsoleTests
{
    class A { }
    class B : A { }
    class C : B, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
    class D : C { }

    class Program
    {
        static void Main(string[] args)
        {
            TypeManager tm = new TypeManager(typeof(A).Assembly);
            //var t0 = tm.Inherits<B>();
            //var t1 = tm.InheritsOrEquals<B>();
            //var t2 = tm.BaseTypesOf<D>();
            //var t3 = tm.Implements<IDisposable>();

            Test0();
        }

        private static void Test0()
        {
            using (CocosRuntime cr = new CocosRuntime())
            {
                cr.Initialized += Cr_Initialized;
                cr.Initialize(true);
                Console.ReadLine();
            }
        }

        private static void Cr_Initialized(object sender, EventArgs e)
        {
            Console.WriteLine("Cocos Runtime is now initialized.");
        }
    }
}