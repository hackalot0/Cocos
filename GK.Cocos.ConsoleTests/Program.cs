using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GK.Cocos.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Test0();
            //Test1();
        }

        private static void Test1()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task t = null;
            try
            {
                using (t = new Task(Test_Task, cts.Token))
                {
                    t.Start();
                    Console.ReadLine();
                    cts.Cancel(true);
                    Console.WriteLine("Cancellation requested!");
                    Console.ReadLine();
                    cts.Cancel();
                    Console.WriteLine("Cancellation requested!");
                    Console.ReadLine();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Test_Task()
        {
            try
            {
                Stopwatch s = Stopwatch.StartNew();
                while (true)
                {
                    Console.WriteLine("Running thread since: {0}", s.Elapsed);
                    Thread.Sleep(250);
                }
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("ThreadAbortException catched!");
            }
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