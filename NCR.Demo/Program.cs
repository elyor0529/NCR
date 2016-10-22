using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCR.Library.Utils;

namespace NCR.Demo
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Test().GetAwaiter();

            Console.ReadKey();
        }


        private static async Task Test()
        {
            var watcher = new Stopwatch();

            watcher.Start();
            using (var parser = new IOParser("input.txt"))
            {
                if (await parser.ReadToEnd())
                    await parser.WriteToEnd("output.txt");
            }
            watcher.Stop();
            Console.WriteLine("Elapsed time is {0:g}", watcher.Elapsed);
        }
    }
}
