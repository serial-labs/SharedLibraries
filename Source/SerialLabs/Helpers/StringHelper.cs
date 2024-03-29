﻿using System;
using System.Globalization;
using System.Text;

namespace SerialLabs
{
    /// <summary>
    /// String manipulation helping methods
    /// </summary>    
    public static class StringHelper
    {
        #region Public Methods
        /// <summary>
        /// Removes accents from a string. 
        /// </summary>
        /// <param name="text">i.e. décembre</param>
        /// <returns>i.e. decembre</returns>
        /// <see cref="http://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net"/>
        public static string RemoveDiacritics(this string value)
        {
            string stFormD = value.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        /// <summary>
        /// Removes accents from a string, using encoding 28595 before converting to ASCII.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <see cref="https://en.wikipedia.org/wiki/ISO/IEC_8859-5"/>
        public static string RemoveDiacriticsToASCII(this string s)
        {
            string r = "";
            try
            {
                r = Encoding.ASCII.GetString(Encoding.GetEncoding(28595).GetBytes(s));
            }
            catch { }
            return r;
        }

        /// <summary>
        /// Encode a string in Base64 (UTF8)
        /// </summary>
        /// <param name="decodedValue"></param>
        /// <returns></returns>
        public static string Base64Utf8Encode(string decodedValue)
        {
            byte[] encData_byte = new byte[decodedValue.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(decodedValue);
            return Convert.ToBase64String(encData_byte);
        }
        /// <summary>
        /// Decode a base64 string (UTF8)
        /// </summary>
        /// <param name="encodedValue"></param>
        /// <returns></returns>
        public static string Base64Utf8Decode(string encodedValue)
        {
            if (String.IsNullOrEmpty(encodedValue))
                return null;

            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = null;

            try
            {
                todecode_byte = Convert.FromBase64String(encodedValue);
            }
            catch (FormatException)
            {
                return null;
            }

            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            return new String(decoded_char);
        }
        /// <summary>
        /// Capitalize the first letter of a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CapitalizeFirstLetter(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return value;
            return String.Format(CultureInfo.CurrentCulture, "{0}{1}", value[0].ToString().ToUpper(CultureInfo.CurrentCulture), value.Substring(1, value.Length - 1));
        }
        #endregion
    }
}
