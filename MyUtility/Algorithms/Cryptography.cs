using MyUtility.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace MyUtility.Algorithms
{
    public static class Cryptography
    {
        private static ThreadLocal<SHA256> _sha256 = new ThreadLocal<SHA256>(() => SHA256.Create());

        internal static byte[] AES256Decrypt(this byte[] block, byte[] key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(block, 0, block.Length);
                }
            }
        }

        internal static byte[] AES256Encrypt(this byte[] block, byte[] key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    return encryptor.TransformFinalBlock(block, 0, block.Length);
                }
            }
        }

        internal static byte[] AesDecrypt(this byte[] data, byte[] key, byte[] iv)
        {
            if (data == null || key == null || iv == null) throw new ArgumentNullException();
            if (data.Length % 16 != 0 || key.Length != 32 || iv.Length != 16) throw new ArgumentException();
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.None;
                using (ICryptoTransform decryptor = aes.CreateDecryptor(key, iv))
                {
                    return decryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }
        }

        internal static byte[] AesEncrypt(this byte[] data, byte[] key, byte[] iv)
        {
            if (data == null || key == null || iv == null) throw new ArgumentNullException();
            if (data.Length % 16 != 0 || key.Length != 32 || iv.Length != 16) throw new ArgumentException();
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.None;
                using (ICryptoTransform encryptor = aes.CreateEncryptor(key, iv))
                {
                    return encryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }
        }

        public static byte[] Sha256(this IEnumerable<byte> value)
        {
            return _sha256.Value.ComputeHash(value.ToArray());
        }

        public static byte[] Sha256(this byte[] value, int offset, int count)
        {
            return _sha256.Value.ComputeHash(value, offset, count);
        }

        internal static byte[] ToAesKey(this string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] passwordHash = sha256.ComputeHash(passwordBytes);
                byte[] passwordHash2 = sha256.ComputeHash(passwordHash);
                Array.Clear(passwordBytes, 0, passwordBytes.Length);
                Array.Clear(passwordHash, 0, passwordHash.Length);
                return passwordHash2;
            }
        }

        // random select m numbers from total
        public static List<int> RandomSelect(int m, int total)
        {
            byte[] seed = BytesHelper.GenRandomBytes(256);
            Dictionary<string, int> cache = new Dictionary<string, int>();
            for (int i = 0; i < total; i++)
            {
                string hash = Sha256(BitConverter.GetBytes(i + 1).Concat(seed)).ToHexString();

                //string hash = BitConverter.GetBytes(BitConverter.ToInt32(seed, i % 224) ^ (i + 1)).ToHexString();
                cache.Add(hash, i + 1);
            }

            return cache.OrderBy(p => p.Key).Take(m).Select(p => p.Value).ToList();
        }
    }
}
