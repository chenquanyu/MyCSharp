﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Algorithms
{
    public static class LeetCode
    {

        /// <summary>
        /// 10. Regular Expression Matching
        /// Given an input string (s) and a pattern (p), implement regular expression matching with support for '.' and '*'
        /// </summary>
        public static bool IsMatch(string s, string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return string.IsNullOrEmpty(s);
            }

            bool firstMatch = s.Length > 0 && (s[0] == p[0] || p[0] == '.');

            // deal with '*'
            if (p.Length >= 2 && p[1] == '*')
            {
                return (firstMatch && IsMatch(s.Substring(1), p)) || (IsMatch(s, p.Substring(2)));
            }

            return firstMatch && IsMatch(s.Substring(1), p.Substring(1));
        }

        public static bool IsMatchDP(string s, string p)
        {
            int sLength = s.Length;
            int pLength = p.Length;
            // i, j     s [i,], p [j,]
            bool[,] results = new bool[sLength + 1, pLength + 1];
            results[sLength, pLength] = true;

            for (int i = sLength; i >= 0; i--)
            {
                for (int j = pLength - 1; j >= 0; j--)
                {
                    bool firstMatch = i < sLength && (s[i] == p[j] || p[j] == '.');

                    // deal with '*'
                    if (j + 1 < pLength && p[j + 1] == '*')
                    {
                        results[i, j] = (firstMatch && results[i + 1, j]) || (results[i, j + 2]);
                    }
                    else
                    {
                        results[i, j] = firstMatch && results[i + 1, j + 1];
                    }
                }
            }

            return results[0, 0];
        }


        /// <summary>
        /// 29. Divide Two Integers
        /// Given two integers dividend and divisor, divide two integers without using multiplication, division and mod operator.
        /// </summary>
        public static int Divide(int dividend, int divisor)
        {
            if (dividend == int.MinValue && divisor == -1)
            {
                return int.MaxValue;
            }

            bool isNeg = (dividend > 0) ^ (divisor > 0);
            long dv = Math.Abs((long)dividend);
            long dr = Math.Abs((long)divisor);

            int result = 0;
            for (int i = 31; i >= 0; i--)
            {
                if (dv >> i >= dr)
                {
                    result += 1 << i;
                    dv -= dr << i;
                }
            }

            return isNeg ? -result : result;
        }


        public static int CountHeight(int[] A)
        {
            if (A == null)
            {
                return 0;
            }

            int length = A.Length;
            if (length <= 1)
            {
                return length;
            }

            int left = 0, right = 0, maxPeriod = 1;
            for (int i = 1; i < length; i++)
            {
                if (i == 1)
                {
                    if (A[i] != A[i - 1])
                    {
                        right = 1;
                        maxPeriod = 2;
                    }
                    continue;
                }

                //if ((A[i] > A[i - 1] && A[i - 1] < A[i - 2]) || (A[i] < A[i - 1] && A[i - 1] > A[i - 2]))
                if (A[i - 1] != A[i - 2] && A[i] != A[i - 1] && (A[i] > A[i - 1] ^ A[i - 1] > A[i - 2]))
                {
                    right = i;
                }
                else
                {
                    int temp = right - left + 1;
                    if (temp > maxPeriod) maxPeriod = temp;

                    if (A[i - 1] == A[i])
                    {
                        left = i;
                    }
                    else { left = i - 1; }

                    right = i;
                }

                if (i == length - 1)
                {
                    int temp = right - left + 1;
                    if (temp > maxPeriod) maxPeriod = temp;
                }
            }
            return maxPeriod;
        }



    }
}
