using System;
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







    }
}
