using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Numerics;
using System.Globalization;
using System.Linq;

namespace MyUtility.Algorithms.Tests
{
    [TestClass()]
    public class BasicTests
    {
        [TestMethod()]
        public void ObjectTest()
        {
            var a = new object[] { 1, 2, true };
            var b = new object[] { 1, 2, true };
            Assert.IsTrue(a[0].Equals(b[0]));
            Assert.IsFalse(a[0] == b[0]);
        }

        [TestMethod()]
        public void BitTest()
        {
            uint a = 1;//0b_0000_1111_0000_1111_0000_1111_0000_1100;
            Console.WriteLine($"a: {a}, bit: {Convert.ToString(a, toBase: 2)}");
            Console.WriteLine($"a: {a}, hexstring: {ToHexString(BitConverter.GetBytes(a))}");

            uint b = ~a;
            Console.WriteLine($"~a: {b}, bit: {Convert.ToString(b, toBase: 2)}");
            Console.WriteLine($"~a: {b}, hexstring: {ToHexString(BitConverter.GetBytes(b))}");


            uint c = a << 4;
            Console.WriteLine($"a<<4: {c}, bit: {Convert.ToString(c, toBase: 2)}");
            Console.WriteLine($"a<<4: {c}, hexstring: {ToHexString(BitConverter.GetBytes(c))}");

            string hex = "009bcd";
            byte[] input = HexToBytes(hex);
            Console.WriteLine($"Before shift:{FormatBinary(input)}, Value:{ToUInt(input)}");

            var result = RightShift(hex, 4);
            Console.WriteLine(result);
            byte[] resBytes = HexToBytes(result);
            Console.WriteLine($"After shift:{FormatBinary(resBytes)}, Value:{ToUInt(resBytes)}");
        }

        public string ToHexString(byte[] value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in value)
                sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        public byte[] HexToBytes(string value)
        {
            if (value == null || value.Length == 0)
                return Array.Empty<byte>();
            if (value.Length % 2 == 1)
                throw new FormatException();
            byte[] result = new byte[value.Length / 2];
            for (int i = 0; i < result.Length; i++)
                result[i] = byte.Parse(value.Substring(i * 2, 2), NumberStyles.AllowHexSpecifier);
            return result;
        }

        public string RightShift(string hexString, int count)
        {
            byte[] input = HexToBytes(hexString);
            Array.Reverse(input);
            BigInteger a = new BigInteger(input);
            a >>= count;
            byte[] result = a.ToByteArray();
            Array.Reverse(result);
            return ToHexString(result);
        }

        public string FormatBinary(byte[] value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in value)
                sb.Append(Convert.ToString(b, toBase: 2).PadLeft(8, '0'));
            return sb.ToString();
        }

        public int ToUInt(byte[] bigEndian)
        {
            string hex = ToHexString(bigEndian);
            return int.Parse(hex, NumberStyles.AllowHexSpecifier);

            //byte[] littleEndian = bigEndian.Reverse().ToArray();
            //return BitConverter.ToUInt32(littleEndian, 0);
        }
    }
}