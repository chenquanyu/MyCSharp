using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace MyUtility.Algorithms.Tests
{
    [TestClass()]
    public class CryptographyTests
    {
        [TestMethod()]
        public void RandomSelectTest()
        {
            int times = 1000;
            int total = 2000;
            int[] selectedTimes = new int[total];

            for (int i = 0; i < times; i++)
            {
                var result = Cryptography.RandomSelect(total / 10, total);
                foreach (int item in result)
                {
                    selectedTimes[item - 1]++;
                }
            }

            Console.WriteLine(JsonConvert.SerializeObject(selectedTimes));
        }
    }
}