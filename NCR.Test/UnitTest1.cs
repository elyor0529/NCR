using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCR.Library.Utils;

namespace NCR.Test
{
    [TestClass]
    public class UnitTest1
    {
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

        [TestMethod]
        [Description("test Read/Write")]
        public void TestMethod1()
        {
            Test().GetAwaiter();
        }
    }
}
