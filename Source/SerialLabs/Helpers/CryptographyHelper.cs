using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace SerialLabs
{
    /// <summary>
    /// Provides cryptographic/hashing helping methods
    /// </summary>
    public static class CryptographyHelper
    {
        /// <summary>
        /// Compute the MD5 hash of a UTF-8 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The md5 hashed string</returns>
        public static string ComputeMD5Hash(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            MD5 md5Hasher = null;
            byte[] data = null;
            try
            {
                md5Hasher = MD5.Create();
                data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
            finally
            {
                if (md5Hasher != null)
                {
                    md5Hasher.Dispose();
                    md5Hasher = null;
                }
            }
            return ByteToHex(data);
        }
        /// <summary>
        /// Computes the MD5 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ComputeMD5HashFromFile(string fileName)
        {
            return ComputeMD5HashFromFile(fileName, false);
        }
        /// <summary>
        /// Computes the MD5 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ComputeMD5HashFromFile(string fileName, bool base64)
        {
            FileStream stream = null;
            MD5 hasher = null;

            try
            {
                stream = File.OpenRead(fileName);
                hasher = MD5.Create();
                if (base64)
                {
                    return Convert.ToBase64String(hasher.ComputeHash(stream));
                }
                else
                {
                    return BitConverter.ToString(hasher.ComputeHash(stream)).Replace("-", "");
                }
            }
            finally
            {
                if (hasher != null)
                {
                    hasher.Dispose();
                    hasher = null;
                }
                if (stream != null)
                {
                    stream.Dispose();
                    stream = null;
                }
            }
        }
        /// <summary>
        /// Compute the SHA256 hash of a UTF-8 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ComputeSHA256Hash(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            SHA256 sha = null;
            byte[] data = null;
            try
            {
                sha = SHA256.Create();
                data = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
            finally
            {
                if (sha != null)
                {
                    sha.Dispose();
                    sha = null;
                }

            }
            return ByteToHex(data);
        }
        /// <summary>
        /// Computes the SHA256 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ComputeSHA256HashFromFile(string fileName)
        {
            return ComputeSHA256HashFromFile(fileName, false);
        }
        /// <summary>
        /// Computes the SHA256 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ComputeSHA256HashFromFile(string fileName, bool base64)
        {
            FileStream stream = null;
            SHA256 hasher = null;

            try
            {
                stream = File.OpenRead(fileName);
                hasher = SHA256.Create();
                if (base64)
                {
                    return Convert.ToBase64String(hasher.ComputeHash(stream));
                }
                else
                {
                    return BitConverter.ToString(hasher.ComputeHash(stream)).Replace("-", "");
                }
            }
            finally
            {
                # region Freeing Resources

                if (hasher != null)
                {
                    hasher.Dispose();
                    hasher = null;
                }
                if (stream != null)
                {
                    stream.Dispose();
                    stream = null;
                }

                # endregion
            }
        }
        /// <summary>
        /// Compute the SHA1 hash of a UTF-8 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ComputeSHA1Hash(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            SHA1 sha = null;
            byte[] data = null;
            try
            {
                sha = SHA1.Create();
                data = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
            finally
            {
                if (sha != null)
                {
                    sha.Dispose();
                    sha = null;
                }
            }
            return ByteToHex(data);
        }
        /// <summary>
        /// Computes the SHA1 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ComputeSHA1HashFromFile(string fileName)
        {
            return ComputeSHA1HashFromFile(fileName, false);
        }
        /// <summary>
        /// Computes the SHA1 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ComputeSHA1HashFromFile(string fileName, bool base64)
        {
            FileStream stream = null;
            SHA1 hasher = null;

            try
            {
                stream = File.OpenRead(fileName);
                hasher = SHA1.Create();
                if (base64)
                {
                    return Convert.ToBase64String(hasher.ComputeHash(stream));
                }
                else
                {
                    return BitConverter.ToString(hasher.ComputeHash(stream)).Replace("-", "");
                }
            }
            finally
            {
                # region Freeing Resources

                if (hasher != null)
                {
                    hasher.Dispose();
                    hasher = null;
                }
                if (stream != null)
                {
                    stream.Dispose();
                    stream = null;
                }

                # endregion
            }
        }
        /// <summary>
        /// Returns the Hexadecimal representation of the byte array
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static string ByteToHex(byte[] bits)
        {
            if (bits == null)
                return null;
            StringBuilder hex = new StringBuilder(bits.Length * 2);
            for (int i = 0; i < bits.Length; i++)
            {
                hex.Append(bits[i].ToString("x2"));
            }
            return hex.ToString();
        }
        /// <summary>
        /// This method encodes an UTF-8 string into a Base64 string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncodeToBase64(string value)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// This method decodes a Base64 string into an UTF-8 string
        /// </summary>
        /// <param name="encodedValue"></param>
        /// <returns></returns>
        public static string DecodeBase64(string encodedValue)
        {
            byte[] bytes = System.Convert.FromBase64String(encodedValue);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// This method encodes a string into CRC32
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ComputeCRC32Hash(string value)
        {
            Crc32 crc32 = new Crc32();
            String hash = String.Empty;

            foreach (byte b in crc32.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)))
            {
                hash += b.ToString("x2");
            }

            return hash;
        }
        public static string ComputeCRC16Hash(string value)
        {
            String hash = String.Empty;
            Crc16 crc16 = new Crc16();
            foreach (byte b in crc16.ComputeChecksumBytes(System.Text.Encoding.UTF8.GetBytes(value)))
            {
                hash += b.ToString("x2");
            }

            return hash;
        }
        public static Byte[] ComputeCRC32HashByte(string value)
        {
            Crc32 crc32 = new Crc32();
            String hash = String.Empty;

            return crc32.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
        }




        /// <summary>
        /// Implements a 32-bit CRC hash algorithm compatible with Zip etc.
        /// </summary>
        /// <remarks>
        /// Copyright (c) Damien Guard. All rights reserved.
        /// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
        /// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
        /// Originally published at http://damieng.com/blog/2006/08/08/calculating_crc32_in_c_and_net
        /// Crc32 should only be used for backward compatibility with older file formats
        /// and algorithms. It is not secure enough for new applications.
        /// If you need to call multiple times for the same data either use the HashAlgorithm
        /// interface or remember that the result of one Compute call needs to be ~ (XOR) before
        /// being passed in as the seed for the next Compute call.
        /// </remarks>
        internal sealed class Crc32 : HashAlgorithm
        {
            public const UInt32 DefaultPolynomial = 0xedb88320u;
            public const UInt32 DefaultSeed = 0xffffffffu;

            private static UInt32[] defaultTable;

            private readonly UInt32 seed;
            private readonly UInt32[] table;
            private UInt32 hash;

            public Crc32()
                : this(DefaultPolynomial, DefaultSeed)
            {
            }

            public Crc32(UInt32 polynomial, UInt32 seed)
            {
                table = InitializeTable(polynomial);
                this.seed = hash = seed;
            }

            public override void Initialize()
            {
                hash = seed;
            }

            protected override void HashCore(byte[] buffer, int start, int length)
            {
                hash = CalculateHash(table, hash, buffer, start, length);
            }

            protected override byte[] HashFinal()
            {
                var hashBuffer = UInt32ToBigEndianBytes(~hash);
                HashValue = hashBuffer;
                return hashBuffer;
            }

            public override int HashSize { get { return 32; } }

            public static UInt32 Compute(byte[] buffer)
            {
                return Compute(DefaultSeed, buffer);
            }

            public static UInt32 Compute(UInt32 seed, byte[] buffer)
            {
                return Compute(DefaultPolynomial, seed, buffer);
            }

            public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
            {
                return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
            }

            private static UInt32[] InitializeTable(UInt32 polynomial)
            {
                if (polynomial == DefaultPolynomial && defaultTable != null)
                    return defaultTable;

                var createTable = new UInt32[256];
                for (var i = 0; i < 256; i++)
                {
                    var entry = (UInt32)i;
                    for (var j = 0; j < 8; j++)
                        if ((entry & 1) == 1)
                            entry = (entry >> 1) ^ polynomial;
                        else
                            entry = entry >> 1;
                    createTable[i] = entry;
                }

                if (polynomial == DefaultPolynomial)
                    defaultTable = createTable;

                return createTable;
            }

            private static UInt32 CalculateHash(UInt32[] table, UInt32 seed, IList<byte> buffer, int start, int size)
            {
                var crc = seed;
                for (var i = start; i < size - start; i++)
                    crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
                return crc;
            }

            private static byte[] UInt32ToBigEndianBytes(UInt32 uint32)
            {
                var result = BitConverter.GetBytes(uint32);

                if (BitConverter.IsLittleEndian)
                    Array.Reverse(result);

                return result;
            }
        }
        /// <summary>
        /// http://www.sanity-free.org/134/standard_crc_16_in_csharp.html
        /// </summary>
        public class Crc16
        {
            const ushort polynomial = 0xA001;
            ushort[] table = new ushort[256];

            public ushort ComputeChecksum(byte[] bytes)
            {
                ushort crc = 0;
                for (int i = 0; i < bytes.Length; ++i)
                {
                    byte index = (byte)(crc ^ bytes[i]);
                    crc = (ushort)((crc >> 8) ^ table[index]);
                }
                return crc;
            }

            public byte[] ComputeChecksumBytes(byte[] bytes)
            {
                ushort crc = ComputeChecksum(bytes);
                return BitConverter.GetBytes(crc);
            }

            public Crc16()
            {
                ushort value;
                ushort temp;
                for (ushort i = 0; i < table.Length; ++i)
                {
                    value = 0;
                    temp = i;
                    for (byte j = 0; j < 8; ++j)
                    {
                        if (((value ^ temp) & 0x0001) != 0)
                        {
                            value = (ushort)((value >> 1) ^ polynomial);
                        }
                        else
                        {
                            value >>= 1;
                        }
                        temp >>= 1;
                    }
                    table[i] = value;
                }
            }
        }
    }
}
