using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WALTools;
using WALTools.Extension;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            const int findPrimeNumber = 1500000;
            Console.WriteLine("Finding {0} th/nd/st prime number", findPrimeNumber);
            findPrimeNumber.CalculateWithMemory(PrimeNumber.FindPrimeNumber);
            sw.Stop();
            Console.WriteLine("Time: {0}", sw.ElapsedMilliseconds);
            sw = new Stopwatch();
            sw.Start();
            findPrimeNumber.CalculateWithMemory(PrimeNumber.FindPrimeNumber);
            sw.Stop();
            Console.WriteLine("Time: {0}", sw.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
