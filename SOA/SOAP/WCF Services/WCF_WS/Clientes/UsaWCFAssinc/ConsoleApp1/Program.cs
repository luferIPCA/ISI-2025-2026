using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CalcWs.CalcServiceClient ws = new CalcWs.CalcServiceClient();

            ws.SumAndSubCompleted += Ws_SumAndSubCompleted;
            ws.SumAndSubAsync(2, 4);
            Console.WriteLine("Antes");
            Console.ReadKey();        }

        private static void Ws_SumAndSubCompleted(object sender, CalcWs.SumAndSubCompletedEventArgs e)
        {
            CalcWs.SumSub r = e.Result;
            Console.WriteLine("Sum=" + r.Sum.ToString() + " Sub="+r.Sub);

        }
    }
}
