using GK.Reflection;
using System;

namespace GK.Cocos.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
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