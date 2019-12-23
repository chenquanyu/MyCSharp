using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyUtility.SmartContract
{
    public class GameDraw
    {



    }

    public class Member
    {
        public int Type { get; set; }

        public int Number { get; set; }

        public bool Selected { get; set; }

        public int Grade { get; set; }

        public byte[] ToBytes()
        {
            uint type = ((uint)Type) << 29;
            uint number = ((uint)Number) << 11;
            uint select = ((uint)(Selected ? 1 : 0)) << 10;

            byte[] result = BitConverter.GetBytes(type | number | select);

            return result;
        }


    }
}
