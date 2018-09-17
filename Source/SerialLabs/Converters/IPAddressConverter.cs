using System;
using System.Linq;

namespace SerialLabs.Converters
{
    /// <summary>
    /// Provides utility methods to manipulate ipaddress
    /// </summary>
    public static class IPAddressConverter
    {
        /// <summary>
        /// Converts a dot notated ip address into its <see cref="Int64"/> counterpart.
        /// Takes care of LittleEndian / Big Endian order
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static long ToInt64(string address)
        {
            try
            {
                if (address.EndsWith(":1")) address = "127.0.0.1";
                byte[] ip = address.Split('.').Select(s => Byte.Parse(s)).ToArray();
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(ip);
                }
                return BitConverter.ToUInt32(ip, 0);
            }
            catch { return -1; }
        }

        // Buggy ...

        ///// <summary>
        ///// Converts a ip address long value into its dot notation representation
        ///// Takes care of LittleEndian / Big Endian order
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static string ToAddress(long value)
        //{
        //    byte[] ip = BitConverter.GetBytes(value);
        //    if (BitConverter.IsLittleEndian)
        //    {
        //        Array.Reverse(ip);
        //    }
        //    return new IPAddress(ip).ToString();
        //}
    }


}
