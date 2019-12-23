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
            int times = 10000;
            int[] selectedTimes = new int[100];

            for (int i = 0; i < times; i++)
            {
                var result = Cryptography.RandomSelect(10, 100);
                foreach (int item in result)
                {
                    selectedTimes[item - 1]++;
                }
            }

            Console.WriteLine(JsonConvert.SerializeObject(selectedTimes));
        }
    }
}