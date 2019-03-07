﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Algorithms
{
    public class LeetCode
    {

        /// <summary>
        /// 10. Regular Expression Matching
        /// Given an input string (s) and a pattern (p), implement regular expression matching with support for '.' and '*'
        /// </summary>
        public bool IsMatch(string s, string p)
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

        public bool IsMatchDP(string s, string p)
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
        public int Divide(int dividend, int divisor)
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


        public int CountHeight(int[] A)
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

        /// <summary>
        /// 44. Wildcard Matching
        /// Given an input string (s) and a pattern (p), implement wildcard pattern matching with support for '?' and '*'.
        /// </summary>
        public bool IsMatchWild(string s, string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return string.IsNullOrEmpty(s);
            }

            // shorten p
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < p.Length; i++)
            {
                if (i + 1 < p.Length && p[i] == '*' && p[i + 1] == '*')
                {
                    continue;
                }
                else
                {
                    builder.Append(p[i]);
                }
            }

            p = builder.ToString();

            int sLength = s == null ? 0 : s.Length;
            int pLength = p.Length;

            // s[i,] p[j,]
            bool[,] dp = new bool[sLength + 1, pLength + 1];
            dp[sLength, pLength] = true;

            for (int i = sLength; i >= 0; i--)
                for (int j = pLength - 1; j >= 0; j--)
                {
                    bool firstMatch = i < sLength && (s[i] == p[j] || p[j] == '?');

                    if (p[j] == '*')
                    {
                        dp[i, j] = (i < sLength && dp[i + 1, j]) || dp[i, j + 1];
                    }
                    else
                    {
                        dp[i, j] = firstMatch && dp[i + 1, j + 1];
                    }
                }


            return dp[0, 0];


            //for (int i = 0; i < p.Length - 1; i++)
            //{
            //    if (p[i] == '*' && p[i + 1] == '*')
            //    {
            //        p = p.Substring(1);
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            //bool firstMatch = s.Length > 0 && (s[0] == p[0] || p[0] == '?');

            //if (p[0] == '*')
            //{
            //    return (s.Length > 0 && IsMatch(s.Substring(1), p)) || IsMatch(s, p.Substring(1));
            //}
            //else
            //{
            //    return firstMatch && IsMatch(s.Substring(1), p.Substring(1));
            //}

        }

        /// <summary>
        /// 48. Rotate Image
        /// You are given an n x n 2D matrix representing an image.
        /// Rotate the image by 90 degrees(clockwise).
        /// </summary>
        public void Rotate(int[,] matrix)
        {
            int n = matrix.GetUpperBound(0) + 1;

            int half = n / 2;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    Swap(matrix, i, j, j, i);
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < half; j++)
                {
                    Swap(matrix, i, j, i, n - 1 - j);
                }
            }

        }

        public void Swap(int[,] matrix, int a, int b, int a1, int b1)
        {
            int temp = matrix[a, b];
            matrix[a, b] = matrix[a1, b1];
            matrix[a1, b1] = temp;
        }

        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            foreach (var item in strs)
            {
                string key = new string(item.OrderBy(p => p).ToArray());

                if (dict.ContainsKey(key))
                {
                    dict[key].Add(item);
                }
                else
                {
                    dict.Add(key, new List<string> { item });
                }
            }

            var result = new List<IList<string>>();
            foreach (var item in dict)
            {
                result.Add(item.Value);
            }

            return result;
        }

        /// <summary>
        /// x ^ n = (x ^ 2) ^ (n/2)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public double MyPow(double x, int n)
        {
            if (x == 0)
            {
                return 0;
            }

            long k = n;
            if (k < 0)
            {
                x = 1/x;
                k = -k;
            }

            double result = 1;
            double current = x;
            while (k > 0)
            {
                bool odd = k % 2 == 1;

                if (odd)
                {
                    result *= current;
                }

                current *= current;
                k = k / 2;
            }

            return result;


        }

    }
}
