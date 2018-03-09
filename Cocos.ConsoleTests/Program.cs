using Cocos.Reflection;
using System;
using System.Linq;
using System.Reflection;

namespace Cocos.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager am = new AssemblyManager();
            Random r = new Random();
            Assembly a = am.State.KnownAssemblies.Values.ElementAt(r.Next(am.State.KnownAssemblies.Count));
            TypeManager tm = a.GetTypeManager();
        }
    }
}