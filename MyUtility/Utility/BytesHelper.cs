using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MyUtility.Utility
{
    public static class BytesHelper
    {
        public static string ToHexString(this byte[] value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in value)
                sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        public static byte[] GenRandomBytes(int length)
        {
            byte[] result = new byte[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(result);
            }
            return result;
        }

        public static byte[] Reverse(byte[] value)
        {
            return value.Reverse().ToArray();
        }

        public static string ToBinaryString(this byte[] value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in value)
            {
                stringBuilder.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                stringBuilder.Append(" ");
            }

            return stringBuilder.ToString();
        }
    }
}
