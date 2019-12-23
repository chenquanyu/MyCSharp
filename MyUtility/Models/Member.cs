using System;
using System.Linq;

namespace MyUtility.Models
{
    public class Member
    {
        public int Type { get; set; }

        public int Number { get; set; }

        public bool Selected { get; set; }

        public int Grade { get; set; }

        public byte[] To3Bytes()
        {
            uint type = ((uint)Type) << 29;
            uint number = ((uint)Number) << 11;
            uint select = ((uint)(Selected ? 1 : 0)) << 10;

            byte[] result = BitConverter.GetBytes(type | number | select).Reverse().Take(3).ToArray();

            return result;
        }

        public byte[] To5Bytes()
        {
            uint type = ((uint)Type) << 29;
            uint number = ((uint)Number) << 11;
            uint select = ((uint)(Selected ? 1 : 0)) << 3;

            byte[] result = BitConverter.GetBytes(type | number | select);

            return result;
        }
    }
}
