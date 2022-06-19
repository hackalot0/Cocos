using Cocos.Core.Networking;

namespace Cocos.Win32
{
    public static class TestRunner
    {
        public static void Main()
        {
            var np = new Net.Peer();

            Console.WriteLine("Program ends here!");
            Console.ReadLine();
        }
    }
}