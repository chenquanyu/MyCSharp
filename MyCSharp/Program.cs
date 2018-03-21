using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCSharp.ClrPractice;
using System.Threading;
using System.IO;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

namespace MyCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Task.Factory.StartNew(() => { GCNotification.GCDone += GCNotification_GCDone; });
            //while (true) { Thread.Sleep(1000); GC.Collect(); }
            GCNotification.GCDone += GCNotification_GCDone;
            //Thread.Sleep(2000);
            //GC.Collect(); GC.WaitForPendingFinalizers();
            string obj = "0123";
            List<string> list = new List<string>();
            while (true)
            {
                if (list.Count < 85000)
                {
                    list.Add(obj + new Random().Next(10000));
                }
                else
                {
                    list.Clear();
                }
                Thread.Sleep(1);
            }
            Console.ReadKey();
        }

        private static void GCNotification_GCDone(int obj)
        {
            //File.AppendAllText(@"C:\temp\GCLog.txt",$"{obj}  {DateTime.Now}\r\n");
            Console.WriteLine($"{obj}  {DateTime.Now}");
            Console.WriteLine($"GC.CollectionCount(0):  {GC.CollectionCount(0)}  {DateTime.Now}");
            Console.WriteLine($"GC.CollectionCount(2):  {GC.CollectionCount(2)}  {DateTime.Now}");
            Console.WriteLine($"GC.GetTotalMemory(true):  {GC.GetTotalMemory(false)} {DateTime.Now}");
            Console.WriteLine();
        }


    }
}
