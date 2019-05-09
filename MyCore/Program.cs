﻿
using DBHelper;
using System;

namespace MyCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //var helper = new LevelDBHelper(@"D:\temp\leveldb");
            var helper = new RocksDBHelper(@"D:\temp\rocksdb");

            System.Diagnostics.Stopwatch sp = new System.Diagnostics.Stopwatch();
            sp.Start();
            long mCount = 0;
            while (true)
            {
                helper.Put(Guid.NewGuid().ToString(), "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaeraaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
                if (System.Threading.Interlocked.Increment(ref mCount) % 10000 == 0)
                {
                    Console.WriteLine("{0} has inserted. time use {1}ms. speed {2} rpms", mCount, sp.ElapsedMilliseconds, mCount / sp.ElapsedMilliseconds);
                }
            }

        }
    }
}
