using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCSharp.ClrPractice
{
    public class AwaitSeq
    {
        private async Task<int> DoWork(List<int> mSecond, int a)
        {
            await Task.Delay(1000);
            for (int i = 0; i < a; i++)
            {
                mSecond.Add(a);
            }
            return mSecond.Count;
        }

        public async Task<string> DoWorks()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var i = new List<int>();
            var a = await DoWork(i, 100);
            var b = DoWork(i, 200);
            var c = DoWork(i, 300);
            var d = DoWork(i, 400);
            var e = DoWork(i, 500);
            var result = $"a: {a}, b: {b.Result}, c: {c.Result}, d: {d.Result}, e: {e.Result};";
            sw.Stop();
            return $"Run time: { sw.ElapsedMilliseconds.ToString() }ms, {result}";
        }



    }
}
