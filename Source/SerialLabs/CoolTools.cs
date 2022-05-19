using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
//serialization
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace seriallabs
{
    public static class CoolTools
    {
        public static string CharsToBeEscaped = @"""\'[]()";
        public static char[] CharsToBeEscapedA = CharsToBeEscaped.ToCharArray();
        public static string CharsToBeTrimmed = " \t \uC2A0\v\r\n\b\a\u00A0\xA0";
        public static char[] CharsToBeTrimmedA = CharsToBeTrimmed.ToCharArray();

        public static object ObjToString(object o) { return o == null ? "" : o.ToString(); }

        public static string Utf8ToUnicode(string utf8) { return Encoding.Unicode.GetString(Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Encoding.UTF8.GetBytes(utf8))); }
        public static string UnicodeToUTF8(string unicodeStr) { return Encoding.Default.GetString(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(unicodeStr))); }

        public static string ConvertToFilename(string s)
        {
            char[] toberemoved = { '\\', ':', '/', '?', '*', '.', '"' };
            foreach (char c in toberemoved)
                s = s.Replace(c, '_');
            return s;
        }

        public static string RemoveWhites(this string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!CharsToBeTrimmedA.Contains(c)) sb.Append(c);
            }
            return sb.ToString();
        }

        public static string EscapeStringWithBackslash(string s)
        {
            StringBuilder sb = new StringBuilder(s.Length + 4);
            int j = 0; int k;
            // tant que l'index position d'un caracter à escaper est positif ou nul, 
            // ajouter à sb la partie de chaîne entre la dernière occurence et celle-ci 
            // moins le caractère trouvé, plus le meta caractère '\'
            // ex : "[toto]"
            // j0=0, k0=0
            while ((k = s.IndexOfAny(CharsToBeEscaped.ToCharArray(), j)) >= 0)
            {
                if (k > j) sb.Append(s.Substring(j, k - j));
                sb.Append('\\');
                sb.Append(s[k]);
                j = k + 1;
            }
            sb.Append(s.Substring(j));
            return sb.ToString();
        }

        /// <summary>
        ///     build a complete path from path elements. Adds separating \ when needed.
        ///     Starts with a root (drive or \)
        ///     Returns a string Ending with filename, or '\' if filename is empty
        /// </summary>
        public static string PathBuild(string root, string filename = "", string path1 = "", string path2 = "", string path3 = "", string path4 = "", bool addTrailingSep = true)
        {
            StringBuilder sb = new StringBuilder(root);
            string[] a = new string[] { path1, path2, path3, path4 };

            if (!root.EndsWith("\\")) sb.Append('\\');
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].StartsWith("\\")) sb.Append(a[i].Substring(1)); else sb.Append(a[i]);
                if (!a[i].EndsWith("\\")) sb.Append('\\');
            }
            sb.Append(filename);
            return sb.ToString();
        }


        /// <summary>
        ///     build a complete path from path elements. Adds separating \ when needed.
        ///     Starts with a root (drive or \)
        ///     Returns a string Ending with filename, or '\' if filename is empty
        /// </summary>
        public static string PathAdd(this string path1, string path2)
        {
            if (!path1.EndsWith("\\")) if (path2.StartsWith("\\")) return path1 + path2; else return path1 + '\\' + path2;
            if (path2.StartsWith("\\")) return path1 + path2.Substring(1); else return path1 + path2;
        }

        //public static int getMatching_fw (string sT,string left_expr) {
        public static int getMatching_fw(string s, int charpos)
        {
            char right_char, left_char;
            if (charpos < 0 || charpos >= s.Length) return -1;
            left_char = s[charpos];
            switch (left_char)
            {
                case '(': right_char = ')'; break;
                case '{': right_char = '}'; break;
                case '[': right_char = ']'; break;
                default: right_char = left_char; break;
            }
            int nbOuv = 1;
            for (int i = charpos + 1; i < s.Length; i++)
            {
                if (s[i] == right_char) nbOuv--;
                if (nbOuv == 0) return i; /// matching closing char has been found
                if (s[i] == left_char) nbOuv++; /// opening of new expression inside
            }
            return -1;
        }

        public static List<string> analyseText(string s)
        { return analyseText(s, '{', '}'); }
        public static List<string> analyseText(string s, char cleft, char cright)
        {
            return analyseText(s, new char[] { cleft }, new char[] { cright });
        }

        public static List<string> analyseText(string s, char[] cleftA, char[] crightA)
        {
            List<string> LT = new List<string>();
            int j = 0, k = 0, l;
            int i = 0;
            while ((j = s.IndexOfAny(cleftA, k)) > -1)
            {
                // get the occurrence of '}' at the same level than the '{' at position j
                k = j; l = j;
                do
                {  // "bla {a{b}{{d}{c}}}bla"

                    k = s.IndexOfAny(crightA, k + 1); //
                    l = s.IndexOfAny(cleftA, l + 1);
                } while (l > 0 && l < k);

                if (j > i) LT.Add(s.Substring(i, j - i));
                if (k > j) { LT.Add(s.Substring(j, k - j + 1)); }
                else { i = j; break; }
                i = j = (k + 1);
            }
            // "{}" : i=2 length=2
            // "{}x" : i=2 length=3
            if (s.Length > i) LT.Add(s.Substring(i, s.Length - i));
            return LT;
        }


        /// <summary>
        /// Takes a List and shuffle more or less the elements at Random
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="OriginalList"></param>
        /// <param name="indicesUsed">out list of int representing the randomly drawn indices used for permutations</param>
        /// <param name="strength100">0=>255 indicate the amount of shuffling required (100 to obtain list.count permutations)</param>
        /// <returns>The ordered list of drawn indices, that can be used to ReApply the same shuffle</returns>
        /// 
        public static List<T> shuffle<T>(List<T> OriginalList, Random myRandom = null)
        {
            List<int> lt = new List<int>();
            return shuffle(OriginalList, out lt, 150, myRandom);
        }
        public static List<T> shuffle<T>(List<T> OriginalList, out List<int> indicesUsed, byte strength100, Random myRandom = null)
        { return shuffle<T>(OriginalList, out indicesUsed, strength100, 101, myRandom); }
        public static List<T> shuffle<T>(List<T> OriginalList, out List<int> indicesUsed, byte strength100, byte relativeDivergence, Random myRandom = null)
        {
            //List<ushort> L=new List<ushort>();
            indicesUsed = new List<int>();
            List<T> newList = new List<T>(OriginalList);
            int nbIter = (int)strength100 * OriginalList.Count / 100;
            if (myRandom == null) myRandom = new Random();
            //Random rnd = new Random(); : oldVersion ? tobedeleted
            /*Return Value
            Type: System..::.Int32
            A 32-bit signed integer greater than or equal to minValue and less than maxValue; that is, the range of return values includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
            */
            int a;
            int b;
            int maxD = -1;
            if (relativeDivergence < 100) maxD = 1 + OriginalList.Count * relativeDivergence / 100;
            T tempa; T tempb;
            for (int i = 0; i < nbIter; i++)
            {
                a = myRandom.Next(0, OriginalList.Count);
                if (relativeDivergence >= 100) b = myRandom.Next(0, OriginalList.Count);
                else
                {// have the second item'sT index be relative to the first. that way, you control the more or less mixed.
                    int c = myRandom.Next(-maxD, maxD + 1); //note:minValue (Int32) The inclusive lower bound of the random number returned. "maxValue" (Int32) The EXCLUSIVE upper bound of the random number returned. maxValue must be >= minValue. 
                    b = a + c;
                    if (b < 0 || b >= OriginalList.Count) b = a;
                }
                indicesUsed.Add(a);
                indicesUsed.Add(b);
                tempa = newList.ElementAt(a);
                tempb = newList.ElementAt(b);
                newList[a] = tempb;
                newList[b] = tempa;
                //newList.RemoveAt(a);
                //newList.Insert(b, temp);
            }
            //indicesUsed = new ushort[23];// L.ToArray();
            return newList;
        }

        /// <summary>
        /// Shuffle a generic List with the provided permutations
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="OriginalList"></param>
        /// <param name="indicesToUse">list of indices to use. Can be obtained when first applying "shuffle"</param>
        /// <returns>The shuffled list</returns>
        public static List<T> shuffleReDo<T>(List<T> OriginalList, List<int> indicesToUse)
        {
            List<T> newList = new List<T>(OriginalList);
            int nbIter = (indicesToUse.Count / 2);
            int a;
            int b;
            T tempa; T tempb;
            for (int i = 0; i < nbIter; i++)
            {
                a = indicesToUse.ElementAt(i * 2);
                b = indicesToUse.ElementAt(i * 2 + 1);
                tempa = newList.ElementAt(a);
                tempb = newList.ElementAt(b);
                newList[a] = tempb;
                newList[b] = tempa;
            }
            return newList;
        }

        public static List<int> buildDistinctRandomList(int start, int length)
        {
            List<int> li = new List<int>(length);
            for (int k = start; k < start + length; k++) li.Add(k);
            return shuffle(li);
        }


        public static string formatMyTime(int seconds)
        {
            int nbh = seconds / 3600;
            int nbmin = seconds / 60 % 60;
            int nbs = seconds % 60;
            return formatMyTime(nbh, nbmin, nbs);
        }

        public static string formatMyTime(int h, int min, int s)
        {
            if (h > 0) return (h.ToString() + " h " + min.ToString() + "").Trim();
            if (min > 0) return (min.ToString() + " min " + s.ToString() + "").Trim();
            return s.ToString() + " s";
        }


        public static int userInputLikeTimespan2sec(string myTimespan)
        {
            string userInput = myTimespan.ToLower().Trim();
            userInput = userInput.Replace(" ", "");//remove spaces inside the string
            // \/ ! the longest words first, to not replace just one letter and after find no match
            string[,] replacementArray = { { "hours", "h", "minutes", "min", "m", "seconds", "sec", "s" },
                                           { ":", ":", ":", ":", ":", "", "", "" } };

            for (int i = 2; i < 8; i++)
                userInput = userInput.Replace(replacementArray[0, i], replacementArray[1, i]);
            if (userInput.EndsWith(":")) userInput += "00";
            int pos_semicol = userInput.IndexOf(':');
            if (pos_semicol < 0)
            {
                if (userInput.Contains("h")) userInput = userInput.Replace("h", ":0") + ":00"; //eg "1h"=>"1:0:00" or "1h02"=>"1:002:00"
                else { userInput = "00:00:" + userInput; pos_semicol = 2; }
            }
            else
            {
                if (userInput.Contains("h")) userInput = userInput.Replace("h", ":"); //eg "1h02:01"=>"1:02:01" or "1h2:1"=>"1:2:1"
                else if (userInput.IndexOf(':', pos_semicol + 1) < 0) userInput = "00:" + userInput;
            }
            userInput = userInput.Replace(',', '.');

            //for (int i=0;i<2;i++)
            //   userInput=userInput.Replace (replacementArray [i,0],replacementArray [i,1]);
            TimeSpan ts;
            if (TimeSpan.TryParse(userInput, out ts))
            {
                return (int)(ts.TotalSeconds);
            }
            return -1;
        }

        //====================================================
        //| Downloaded From                                  |
        //| Visual C# Kicks - http://www.vcskicks.com/       |
        //| License - http://www.vcskicks.com/license.html   |
        //====================================================

        public static class ConvertArray2String
        {
            public static string ArrayToString(IList array)
            {
                return ArrayToString(array, Environment.NewLine);
            }

            public static string ArrayToString(IList array, string delimiter)
            {
                //string outputString = "";
                // *** why not string.join() ?? ***
                StringBuilder sb = new StringBuilder();
                string separator = "";
                foreach (object obj in array)
                {
                    sb.Append(separator);
                    sb.Append(obj.ToString());
                    separator = delimiter;

                    //outputString += obj.ToString() + delimiter ;
                }

                return sb.ToString();// outputString;
            }



            public static string ArrayToStringGeneric<T>(IList<T> array)
            {
                return ArrayToStringGeneric<T>(array, Environment.NewLine);
            }

            public static string ArrayToStringGeneric<T>(IList<T> array, string delimiter)
            {
                return ArrayToString(array.ToArray(), delimiter);
                /*//string outputString = "";
                StringBuilder sb = new StringBuilder();
                string separator = "";
                foreach (object obj in array)
                {
                    sb.Append(separator);
                    sb.Append(obj.ToString());
                    separator = delimiter;

                    //outputString += obj.ToString() + delimiter ;
                }

                return sb.ToString();// outputString;*/
            }
        }

        /************************************************/
        /// <summary>
        /// Transforme une chaine représentant adresse IP en entier 32bit
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static int IPToInt(string ipAddress)
        {
            int num = 0;
            try
            {
                string[] ipBytes;
                byte temp = 0;
                int exp = 24;
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    ipBytes = ipAddress.Split('.');
                    for (int i = 0; i <= ipBytes.Length - 1; i++)
                    {
                        temp = byte.Parse(ipBytes[i]);
                        num += temp << exp;
                        exp -= 8;
                    }
                }
            }
            catch { num = -1; }
            return num;
        }
        /**************en T-SQL ***********
   * 
FUNCTION [ada].[IPAddressToInteger] (@IP AS varchar(15))
RETURNS int
AS
BEGIN
declare @a bigint
declare @b bigint
 
set @a = 
  (CONVERT(bigint, PARSENAME(@IP,1)) +
   CONVERT(bigint, PARSENAME(@IP,2)) * 256 +
   CONVERT(bigint, PARSENAME(@IP,3)) * 65536 +
   CONVERT(bigint, PARSENAME(@IP,4)) * 16777216) 
if (@a>2147483647) BEGIN
set @a = @a-4294967296
END
 
   --)
RETURN convert (int, @a)
END*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intIP"></param>
        /// <returns></returns>
        public static string IntToIP(int intIP)
        {
            string ipAddress = "";
            byte[] byteArray = new byte[4];
            int num, exp = 24;
            for (int i = 0; i < 4; i++)
            {
                num = intIP >> exp;
                exp -= 8;
                byteArray[i] = (byte)(num & 255);
            }
            ipAddress = byteArray[0].ToString();
            for (int i = 1; i < 4; i++)
            {
                ipAddress += "." + byteArray[i].ToString();
            }
            return ipAddress;
        }
        /**************en T-SQL ***********
		 * 
  FUNCTION [ada].[IntegerToIPAddress] (@IP AS int)
RETURNS varchar(15)
AS
BEGIN
 DECLARE @Octet1 bigint
 DECLARE @Octet2 tinyint
 DECLARE @Octet3 tinyint
 DECLARE @Octet4 tinyint
 DECLARE @RestOfIP bigint
 
 declare @bi bigint
 
 set @bi = convert(bigint,@IP)  -- +2147483648
 if (@IP<0) set @bi = convert(bigint,@IP+4294967296)
 
 
 SET @Octet1 = @bi / 16777216
 SET @RestOfIP = @bi - (@Octet1 * 16777216)
 SET @Octet2 = @RestOfIP / 65536
 SET @RestOfIP = @RestOfIP - (@Octet2 * 65536)
 SET @Octet3 = @RestOfIP / 256
 SET @Octet4 = @RestOfIP - (@Octet3 * 256)
 
 RETURN(CONVERT(varchar, @Octet1) + '.' +
		CONVERT(varchar, @Octet2) + '.' +
		CONVERT(varchar, @Octet3) + '.' +
		CONVERT(varchar, @Octet4))
END
*/




        /// <summary>
        /// combines two string forming a url or uri; takes care of having just one '/' between them
        /// </summary>
        /// <param name="leftPart"></param>
        /// <param name="rightPart"></param>
        /// <returns></returns>
        public static string UriAddto(this string leftPart, string rightPart)
        {
            string result = leftPart;
            if (leftPart.EndsWith("/")) result = result.Remove(result.Length - 1);
            if (rightPart.StartsWith("/")) result = result + rightPart; else result = result + "/" + rightPart;
            return result;
        }

        /*************************************************************/


        public static int getFirstIntInsideString(string s) { return getFirstIntInsideString(s, true); }
        public static int getFirstIntInsideString(string s, bool beTolerant)
        {
            double d = Math.Round(getFirstFloatInsideString(s, beTolerant), MidpointRounding.ToEven);
            if (double.IsNaN(d)) return 0;
            return (int)d;
        }
        public static float getFirstFloatInsideString(string s)
        { return getFirstFloatInsideString(s, true); }
        public static float getFirstFloatInsideString(string s, bool beTolerant)
        {
            float a = float.NaN;
            string n = "";
            s = s.Trim(CharsToBeTrimmedA);
            bool readingNBhasbegun = false;
            foreach (char c in s)
            {
                if (char.IsDigit(c))
                {
                    readingNBhasbegun = true;
                    n += c;
                }
                else if ("+-,.".Contains(c))
                    n += c;
                else if (!" \t".Contains(c))
                {
                    /// non-digit is found. if numberreading has begun, OR we should not 
                    /// be tolerant, then it stops here. Else continue 
                    if (readingNBhasbegun || !beTolerant) break;
                }/// else, light separator is found. number may continue as in "1 200" = 1200
            }
            /// ToDo : gerer la virgule , le point ...
            //float.TryParse(n, out a);
            try
            {
                if (float.TryParse(n, out a))
                {
                    a = float.Parse(n);
                }

            }
            catch (Exception) { }
            return a;
        }


        public static int getFirstIntUseRegex(this string s)
        {
            if (s == null) return 0;
            var exp = new Regex("(\\d+)"); // find a sequence of digits could be \d+
            var matches = exp.Matches(s);
            if (matches.Count > 0) // if there's something return the first one
            {
                int number = int.Parse(matches[0].Value);
                return number;
            }
            else return 0;
        }

        // doesn't seem to work well
        /*public static int getFirstIntInsideString(this string s)
        {
          double d = Math.Round(getFirstFloatInsideString(s ?? "0"), MidpointRounding.ToEven);
          if (double.IsNaN(d)) return 0;
          return (int)d;
        }*/

        /*OLI:2022-03:doublon utile? 
        public static float getFirstFloatInsideString(this string s)
        {
          float a = float.NaN;
          string n = "";
          if (!String.IsNullOrEmpty(s))
            foreach (char c in s)
            {
              if (char.IsDigit(c))
                n += c;
              else if ("+-,.".Contains(c))
                n += c;
              else if (!" \t".Contains(c)) break;
            }
          float.TryParse(n, out a);
          return a;
        }*/
        /****************************************************/
        /// http://blogs.msdn.com/toub/archive/2006/05/05/590814.aspx
        /// <SUMMARY>Computes the Levenshtein Edit Distance between two enumerables.</SUMMARY>
        /// <TYPEPARAM name="T">The type of the items in the enumerables.</TYPEPARAM>
        /// <PARAM name="x">The first enumerable.</PARAM>
        /// <PARAM name="y">The second enumerable.</PARAM>
        /// <RETURNS>The edit distance.</RETURNS>
        public static int EditDistance<T>(IEnumerable<T> x, IEnumerable<T> y)
            where T : IEquatable<T>
        {
            return EditDistance(x, y, true, false);
        }
        public static int EditDistance<T>(IEnumerable<T> x, IEnumerable<T> y, bool allowForSubstitutions, bool allowPlaceHolder)
            where T : IEquatable<T>
        {
            // Validate parameters
            if (x == null) throw new ArgumentNullException("x??");
            if (y == null) throw new ArgumentNullException("y??");

            // Convert the parameters into IList instances
            // in order to obtain indexing capabilities
            IList<T> first = x as IList<T> ?? new List<T>(x);
            IList<T> second = y as IList<T> ?? new List<T>(y);

            // Get the length of both.  If either is 0, return
            // the length of the other, since that number of insertions
            // would be required.
            int n = first.Count, m = second.Count;
            if (n == 0) return m;
            if (m == 0) return n;

            // Rather than maintain an entire matrix (which would require O(n*m) space),
            // just store the current row and the next row, each of which has a length m+1,
            // so just O(m) space. Initialize the current row.
            int curRow = 0, nextRow = 1;
            int[][] rows = new int[][] { new int[m + 1], new int[m + 1] };
            for (int j = 0; j <= m; ++j) rows[curRow][j] = j;

            // For each virtual row (since we only have physical storage for two)
            for (int i = 1; i <= n; ++i)
            {
                // Fill in the values in the row
                rows[nextRow][0] = i;
                for (int j = 1; j <= m; ++j)
                {
                    int dist1 = rows[curRow][j] + 1;
                    int dist2 = rows[nextRow][j - 1] + 1;
                    int d3 = first[i - 1].Equals(second[j - 1]) ? 0 : 1;
                    if (allowForSubstitutions) // then may reduce d3 if substitution detected, else leave unchanged
                        if (d3 == 1) if (i < n && j < m)
                                if (first[i].Equals(second[j - 1]) && first[i - 1].Equals(second[j])) d3 = 0;
                    if (allowPlaceHolder)
                        if (typeof(T) == typeof(char))
                            if (d3 == 1) if (first[i - 1].Equals('?') || first[i - 2 < 0 ? 0 : i - 2].Equals('?')) d3 = 0;
                    int dist3 = rows[curRow][j - 1] + d3; //original version: "+ (first[i - 1].Equals(second[j - 1]) ? 0 : 1);"
                    //dist3 += d3;
                    rows[nextRow][j] = Math.Min(dist1, Math.Min(dist2, dist3));
                }

                // Swap the current and next rows
                if (curRow == 0)
                {
                    curRow = 1;
                    nextRow = 0;
                }
                else
                {
                    curRow = 0;
                    nextRow = 1;
                }
            }

            // Return the computed edit distance
            return rows[curRow][m];
        }

        public struct simpletoken
        {
            bool facultatif;
            string text;
            public List<simpletoken> lst;

            public simpletoken(bool isBlock) { this.facultatif = isBlock; this.text = ""; this.lst = null; }
            public simpletoken(bool isBlock, string content) { this.facultatif = isBlock; this.text = content; this.lst = null; }

            public IEnumerable<string> getVariantesLongestFirst
            { get { return _getVariantesLF(this, 0, ""); } }
            public IEnumerable<string> getVariantesLongestFirstWSep
            { get { return _getVariantesLF(this, 0, " "); } }

            public IEnumerable<string> getVariantes
            { get { return _getVariantes(this, 0, ""); } }
            public IEnumerable<string> getVariantesWSep //With Separator
            { get { return _getVariantes(this, 0, " "); } }
            public IEnumerable<string> getVariantesEx(string separator)
            { return _getVariantes(this, 0, separator); }

            private IEnumerable<string> _getVariantes(simpletoken stok, int idx, string separator)
            {
                /// petit algo sympa pour récupérer toutes les variantes !!
                /// il est certainement possible de faire plus clair (avec l'arbre)
                /// mais c'est déjà pas si mal ...
                if (stok.lst == null)
                { ///leaf
                    if (stok.facultatif) yield return "";
                    yield return stok.text;
                }
                else
                {
                    if (stok.facultatif && idx == 0) yield return "";
                    foreach (string sv in stok.lst[idx].getVariantes)
                    {
                        if (idx + 1 == stok.lst.Count)
                            yield return sv;
                        else
                            foreach (string svs in stok._getVariantes(stok, idx + 1, separator))
                            {
                                //if (!string.IsNullOrEmpty(separator)) { sv = sv.TrimEnd(separator); svs = svs.TrimStart(separator); }
                                bool inclSep = !(sv.EndsWith(separator) || svs.StartsWith(separator));
                                yield return sv + (inclSep ? separator : "") + svs;
                            }
                    }
                }
            }
            private IEnumerable<string> _getVariantesLF(simpletoken stok, int idx, string separator)
            {
                /// petit algo sympa pour récupérer toutes les variantes !!
                /// il est certainement possible de faire plus clair (avec l'arbre)
                /// mais c'est déjà pas si mal ...
                if (stok.lst == null)
                { ///leaf
                    yield return stok.text;
                    if (stok.facultatif) yield return "";
                }
                else
                {
                    foreach (string sv in stok.lst[idx].getVariantesLongestFirst)
                    {
                        if (idx + 1 == stok.lst.Count)
                            yield return sv;
                        else
                            foreach (string svs in stok._getVariantesLF(stok, idx + 1, separator))
                            {
                                bool inclSep = !(sv.EndsWith(separator) || svs.StartsWith(separator));
                                yield return sv + (inclSep ? separator : "") + svs;
                            }
                    }
                    if (stok.facultatif && idx == 0) yield return "";

                }
            }

            public void analyseExpression(string s)
            {
                //List<simpletoken> l=null;
                int j = 0, k;
                int i = s.IndexOfAny(new char[] { '[', '(' });
                if (i == -1)
                    this.text = s;
                else //contains subblock 
                {
                    lst = new List<simpletoken>();
                    do
                    {
                        if (i > j) lst.Add(new simpletoken(false, s.Substring(j, i - j)));
                        k = getMatching_fw(s, i);
                        if (k == -1) k = s.Length;
                        simpletoken ns = new simpletoken(true);
                        ns.analyseExpression(s.Substring(i + 1, k - i - 1)); //exclude facultatif markers!
                        lst.Add(ns);
                        j = k + 1;
                        i = s.IndexOfAny(new char[] { '[', '(' }, j);
                    } while (i > -1);
                    if (j < s.Length) lst.Add(new simpletoken(false, s.Substring(j)));
                }
                //return l;
            }

            /// <summary>
            /// return
            /// </summary>
            /// <param name="ls"></param>
            /// <returns></returns>
            /*public int checkIfStrListHasNonNullElts (List<string> ls) {
                ; 
            }*/


        }


        private static readonly string[][] htmlNamedEntities = new string[][] {
            new string[] { "&quot;", "\"" },
            new string[] { "&lt;", "<" },
            new string[] { "&gt;", ">" },
            new string[] { "&nbsp;", " " },
            new string[] { "&iexcl;", "¡" },
            new string[] { "&cent;", "¢" },
            new string[] { "&pound;", "£" },
            new string[] { "&curren;", "¤" },
            new string[] { "&yen;", "¥" },
            new string[] { "&brvbar;", "¦" },
            new string[] { "&sect;", "§" },
            new string[] { "&uml;", "¨" },
            new string[] { "&copy;", "©" },
            new string[] { "&ordf;", "ª" },
            new string[] { "&laquo;", "«" },
            new string[] { "&not;", "¬" },
            new string[] { "&shy;", "­" },
            new string[] { "&reg;", "®" },
            new string[] { "&macr;", "¯" },
            new string[] { "&deg;", "°" },
            new string[] { "&plusmn;", "±" },
            new string[] { "&sup2;", "²" },
            new string[] { "&sup3;", "³" },
            new string[] { "&acute;", "´" },
            new string[] { "&micro;", "µ" },
            new string[] { "&para;", "¶" },
            new string[] { "&middot;", "·" },
            new string[] { "&cedil;", "¸" },
            new string[] { "&sup1;", "¹" },
            new string[] { "&ordm;", "º" },
            new string[] { "&raquo;", " »" },
            new string[] { "&frac14;", "¼" },
            new string[] { "&frac12;", "½" },
            new string[] { "&frac34;", "¾" },
            new string[] { "&iquest;", "¿" },
            new string[] { "&Agrave;", "À" },
            new string[] { "&Aacute;", "Á" },
            new string[] { "&Acirc;", "Â" },
            new string[] { "&Atilde;", "Ã" },
            new string[] { "&Auml;", "Ä" },
            new string[] { "&Aring;", "Å" },
            new string[] { "&AElig;", "Æ" },
            new string[] { "&Ccedil;", "Ç" },
            new string[] { "&Egrave;", "È" },
            new string[] { "&Eacute;", "É" },
            new string[] { "&Ecirc;", "Ê" },
            new string[] { "&Euml;", "Ë" },
            new string[] { "&Igrave;", "Ì" },
            new string[] { "&Iacute;", "Í" },
            new string[] { "&Icirc;", "Î" },
            new string[] { "&Iuml;", "Ï" },
            new string[] { "&ETH;", "Ð" },
            new string[] { "&Ntilde;", "Ñ" },
            new string[] { "&Ograve;", "Ò" },
            new string[] { "&Oacute;", "Ó" },
            new string[] { "&Ocirc;", "Ô" },
            new string[] { "&Otilde;", "Õ" },
            new string[] { "&Ouml;", "Ö" },
            new string[] { "&times;", "×" },
            new string[] { "&Oslash;", "Ø" },
            new string[] { "&Ugrave;", "Ù" },
            new string[] { "&Uacute;", "Ú" },
            new string[] { "&Ucirc;", "Û" },
            new string[] { "&Uuml;", "Ü" },
            new string[] { "&Yacute;", "Ý" },
            new string[] { "&THORN;", "Þ" },
            new string[] { "&szlig;", "ß" },
            new string[] { "&agrave;", "à" },
            new string[] { "&aacute;", "á" },
            new string[] { "&acirc;", "â" },
            new string[] { "&atilde;", "ã" },
            new string[] { "&auml;", "ä" },
            new string[] { "&aring;", "å" },
            new string[] { "&aelig;", "æ" },
            new string[] { "&ccedil;", "ç" },
            new string[] { "&egrave;", "è" },
            new string[] { "&eacute;", "é" },
            new string[] { "&ecirc;", "ê" },
            new string[] { "&euml;", "ë" },
            new string[] { "&igrave;", "ì" },
            new string[] { "&iacute;", "í" },
            new string[] { "&icirc;", "î" },
            new string[] { "&iuml;", "ï" },
            new string[] { "&eth;", "ð" },
            new string[] { "&ntilde;", "ñ" },
            new string[] { "&ograve;", "ò" },
            new string[] { "&oacute;", "ó" },
            new string[] { "&ocirc;", "ô" },
            new string[] { "&otilde;", "õ" },
            new string[] { "&ouml;", "ö" },
            new string[] { "&divide;", "÷" },
            new string[] { "&oslash;", "ø" },
            new string[] { "&ugrave;", "ù" },
            new string[] { "&uacute;", "ú" },
            new string[] { "&ucirc;", "û" },
            new string[] { "&uuml;", "ü" },
            new string[] { "&yacute;", "ý" },
            new string[] { "&thorn;", "þ" },
            new string[] { "&yuml;", "ÿ" },
            new string[] { "&OElig;", "Œ" },
            new string[] { "&oelig;", "œ" },
            new string[] { "&Scaron;", "Š" },
            new string[] { "&scaron;", "š" },
            new string[] { "&Yuml;", "Ÿ" },
            new string[] { "&fnof;", "ƒ" },
            new string[] { "&circ;", "ˆ" },
            new string[] { "&tilde;", "˜" },
            new string[] { "&Alpha;", "Α" },
            new string[] { "&Beta;", "Β" },
            new string[] { "&Gamma;", "Γ" },
            new string[] { "&Delta;", "Δ" },
            new string[] { "&Epsilon;", "Ε" },
            new string[] { "&Zeta;", "Ζ" },
            new string[] { "&Eta;", "Η" },
            new string[] { "&Theta;", "Θ" },
            new string[] { "&Iota;", "Ι" },
            new string[] { "&Kappa;", "Κ" },
            new string[] { "&Lambda;", "Λ" },
            new string[] { "&Mu;", "Μ" },
            new string[] { "&Nu;", "Ν" },
            new string[] { "&Xi;", "Ξ" },
            new string[] { "&Omicron;", "Ο" },
            new string[] { "&Pi;", "Π" },
            new string[] { "&Rho;", "Ρ" },
            new string[] { "&Sigma;", "Σ" },
            new string[] { "&Tau;", "Τ" },
            new string[] { "&Upsilon;", "Υ" },
            new string[] { "&Phi;", "Φ" },
            new string[] { "&Chi;", "Χ" },
            new string[] { "&Psi;", "Ψ" },
            new string[] { "&Omega;", "Ω" },
            new string[] { "&alpha;", "α" },
            new string[] { "&beta;", "β" },
            new string[] { "&gamma;", "γ" },
            new string[] { "&delta;", "δ" },
            new string[] { "&epsilon;", "ε" },
            new string[] { "&zeta;", "ζ" },
            new string[] { "&eta;", "η" },
            new string[] { "&theta;", "θ" },
            new string[] { "&iota;", "ι" },
            new string[] { "&kappa;", "κ" },
            new string[] { "&lambda;", "λ" },
            new string[] { "&mu;", "μ" },
            new string[] { "&nu;", "ν" },
            new string[] { "&xi;", "ξ" },
            new string[] { "&omicron;", "ο" },
            new string[] { "&pi;", "π" },
            new string[] { "&rho;", "ρ" },
            new string[] { "&sigmaf;", "ς" },
            new string[] { "&sigma;", "σ" },
            new string[] { "&tau;", "τ" },
            new string[] { "&upsilon;", "υ" },
            new string[] { "&phi;", "φ" },
            new string[] { "&chi;", "χ" },
            new string[] { "&psi;", "ψ" },
            new string[] { "&omega;", "ω" },
            new string[] { "&thetasym;", "ϑ" },
            new string[] { "&upsih;", "ϒ" },
            new string[] { "&piv;", "ϖ" },
            new string[] { "&ensp;", " " },
            new string[] { "&emsp;", " " },
            new string[] { "&thinsp;", " " },
            new string[] { "&zwnj;", "‌" },
            new string[] { "&zwj;", "‍" },
            new string[] { "&lrm;", "‎" },
            new string[] { "&rlm;", "‏" },
            new string[] { "&ndash;", "–" },
            new string[] { "&mdash;", "—" },
            new string[] { "&lsquo;", "‘" },
            new string[] { "&rsquo;", "’" },
            new string[] { "&sbquo;", "‚" },
            new string[] { "&ldquo;", "“" },
            new string[] { "&rdquo;", "”" },
            new string[] { "&bdquo;", "„" },
            new string[] { "&dagger;", "†" },
            new string[] { "&Dagger;", "‡" },
            new string[] { "&bull;", "•" },
            new string[] { "&hellip;", "…" },
            new string[] { "&permil;", "‰" },
            new string[] { "&prime;", "′" },
            new string[] { "&Prime;", "″" },
            new string[] { "&lsaquo;", "‹" },
            new string[] { "&rsaquo;", "›" },
            new string[] { "&oline;", "‾" },
            new string[] { "&frasl;", "⁄" },
            new string[] { "&euro;", "€" },
            new string[] { "&image;", "ℑ" },
            new string[] { "&weierp;", "℘" },
            new string[] { "&real;", "ℜ" },
            new string[] { "&trade;", "™" },
            new string[] { "&alefsym;", "ℵ" },
            new string[] { "&larr;", "←" },
            new string[] { "&uarr;", "↑" },
            new string[] { "&rarr;", "→" },
            new string[] { "&darr;", "↓" },
            new string[] { "&harr;", "↔" },
            new string[] { "&crarr;", "↵" },
            new string[] { "&lArr;", "⇐" },
            new string[] { "&uArr;", "⇑" },
            new string[] { "&rArr;", "⇒" },
            new string[] { "&dArr;", "⇓" },
            new string[] { "&hArr;", "⇔" },
            new string[] { "&forall;", "∀" },
            new string[] { "&part;", "∂" },
            new string[] { "&exist;", "∃" },
            new string[] { "&empty;", "∅" },
            new string[] { "&nabla;", "∇" },
            new string[] { "&isin;", "∈" },
            new string[] { "&notin;", "∉" },
            new string[] { "&ni;", "∋" },
            new string[] { "&prod;", "∏" },
            new string[] { "&sum;", "∑" },
            new string[] { "&minus;", "−" },
            new string[] { "&lowast;", "∗" },
            new string[] { "&radic;", "√" },
            new string[] { "&prop;", "∝" },
            new string[] { "&infin;", "∞" },
            new string[] { "&ang;", "∠" },
            new string[] { "&and;", "∧" },
            new string[] { "&or;", "∨" },
            new string[] { "&cap;", "∩" },
            new string[] { "&cup;", "∪" },
            new string[] { "&int;", "∫" },
            new string[] { "&there4;", "∴" },
            new string[] { "&sim;", "∼" },
            new string[] { "&cong;", "≅" },
            new string[] { "&asymp;", "≈" },
            new string[] { "&ne;", "≠" },
            new string[] { "&equiv;", "≡" },
            new string[] { "&le;", "≤" },
            new string[] { "&ge;", "≥" },
            new string[] { "&sub;", "⊂" },
            new string[] { "&sup;", "⊃" },
            new string[] { "&nsub;", "⊄" },
            new string[] { "&sube;", "⊆" },
            new string[] { "&supe;", "⊇" },
            new string[] { "&oplus;", "⊕" },
            new string[] { "&otimes;", "⊗" },
            new string[] { "&perp;", "⊥" },
            new string[] { "&sdot;", "⋅" },
            new string[] { "&lceil;", "⌈" },
            new string[] { "&rceil;", "⌉" },
            new string[] { "&lfloor;", "⌊" },
            new string[] { "&rfloor;", "⌋" },
            new string[] { "&lang;", "〈" },
            new string[] { "&rang;", "〉" },
            new string[] { "&loz;", "◊" },
            new string[] { "&spades;", "♠" },
            new string[] { "&clubs;", "♣" },
            new string[] { "&hearts;", "♥" },
            new string[] { "&diams;", "♦" },
            new string[] { "&amp;", "&" }
        };
        /// <summary>
        /// http://www.codeproject.com/KB/MCMS/htmlTagStripper.aspx
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <param name="replaceNamedEntities"></param>
        /// <param name="replaceNumberedEntities"></param>
        /// <returns></returns>
        /// <permission cref="http://www.codeproject.com/KB/MCMS/htmlTagStripper.aspx"> </permission>
        public static string HtmlStripTags(string htmlContent, bool replaceNamedEntities, bool replaceNumberedEntities)
        {
            if (htmlContent == null)
                return null;
            htmlContent = htmlContent.Trim();
            if (htmlContent == string.Empty)
                return string.Empty;

            int bodyStartTagIdx = htmlContent.IndexOf("<body", StringComparison.CurrentCultureIgnoreCase);
            int bodyEndTagIdx = htmlContent.IndexOf("</body>", StringComparison.CurrentCultureIgnoreCase);

            int startIdx = 0, endIdx = htmlContent.Length - 1;
            if (bodyStartTagIdx >= 0)
                startIdx = bodyStartTagIdx;
            if (bodyEndTagIdx >= 0)
                endIdx = bodyEndTagIdx;

            bool insideTag = false,
                    insideAttributeValue = false,
                    insideHtmlComment = false,
                    insideScriptBlock = false,
                    insideNoScriptBlock = false,
                    insideStyleBlock = false;
            char attributeValueDelimiter = '"';

            StringBuilder sb = new StringBuilder(htmlContent.Length);
            for (int i = startIdx; i <= endIdx; i++)
            {

                // html comment block
                if (!insideHtmlComment)
                {
                    if (i + 3 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        htmlContent[i + 1] == '!' &&
                        htmlContent[i + 2] == '-' &&
                        htmlContent[i + 3] == '-')
                    {
                        i += 3;
                        insideHtmlComment = true;
                        continue;
                    }
                }
                else // inside html comment
                {
                    if (i + 2 < htmlContent.Length &&
                        htmlContent[i] == '-' &&
                        htmlContent[i + 1] == '-' &&
                        htmlContent[i + 2] == '>')
                    {
                        i += 2;
                        insideHtmlComment = false;
                        continue;
                    }
                    else
                        continue;
                }

                // noscript block
                if (!insideNoScriptBlock)
                {
                    if (i + 9 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        (htmlContent[i + 1] == 'n' || htmlContent[i + 1] == 'N') &&
                        (htmlContent[i + 2] == 'o' || htmlContent[i + 2] == 'O') &&
                        (htmlContent[i + 3] == 's' || htmlContent[i + 3] == 'S') &&
                        (htmlContent[i + 4] == 'c' || htmlContent[i + 4] == 'C') &&
                        (htmlContent[i + 5] == 'r' || htmlContent[i + 5] == 'R') &&
                        (htmlContent[i + 6] == 'i' || htmlContent[i + 6] == 'I') &&
                        (htmlContent[i + 7] == 'p' || htmlContent[i + 7] == 'P') &&
                        (htmlContent[i + 8] == 't' || htmlContent[i + 8] == 'T') &&
                        (char.IsWhiteSpace(htmlContent[i + 9]) || htmlContent[i + 9] == '>'))
                    {
                        i += 9;
                        insideNoScriptBlock = true;
                        continue;
                    }
                }
                else // inside noscript block
                {
                    if (i + 10 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        htmlContent[i + 1] == '/' &&
                        (htmlContent[i + 2] == 'n' || htmlContent[i + 2] == 'N') &&
                        (htmlContent[i + 3] == 'o' || htmlContent[i + 3] == 'O') &&
                        (htmlContent[i + 4] == 's' || htmlContent[i + 4] == 'S') &&
                        (htmlContent[i + 5] == 'c' || htmlContent[i + 5] == 'C') &&
                        (htmlContent[i + 6] == 'r' || htmlContent[i + 6] == 'R') &&
                        (htmlContent[i + 7] == 'i' || htmlContent[i + 7] == 'I') &&
                        (htmlContent[i + 8] == 'p' || htmlContent[i + 8] == 'P') &&
                        (htmlContent[i + 9] == 't' || htmlContent[i + 9] == 'T') &&
                        (char.IsWhiteSpace(htmlContent[i + 10]) || htmlContent[i + 10] == '>'))
                    {
                        if (htmlContent[i + 10] != '>')
                        {
                            i += 9;
                            while (i < htmlContent.Length && htmlContent[i] != '>')
                                i++;
                        }
                        else
                            i += 10;
                        insideNoScriptBlock = false;
                    }
                    continue;
                }

                // script block
                if (!insideScriptBlock)
                {
                    if (i + 7 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        (htmlContent[i + 1] == 's' || htmlContent[i + 1] == 'S') &&
                        (htmlContent[i + 2] == 'c' || htmlContent[i + 2] == 'C') &&
                        (htmlContent[i + 3] == 'r' || htmlContent[i + 3] == 'R') &&
                        (htmlContent[i + 4] == 'i' || htmlContent[i + 4] == 'I') &&
                        (htmlContent[i + 5] == 'p' || htmlContent[i + 5] == 'P') &&
                        (htmlContent[i + 6] == 't' || htmlContent[i + 6] == 'T') &&
                        (char.IsWhiteSpace(htmlContent[i + 7]) || htmlContent[i + 7] == '>'))
                    {
                        i += 6;
                        insideScriptBlock = true;
                        continue;
                    }
                }
                else // inside script block
                {
                    if (i + 8 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        htmlContent[i + 1] == '/' &&
                        (htmlContent[i + 2] == 's' || htmlContent[i + 2] == 'S') &&
                        (htmlContent[i + 3] == 'c' || htmlContent[i + 3] == 'C') &&
                        (htmlContent[i + 4] == 'r' || htmlContent[i + 4] == 'R') &&
                        (htmlContent[i + 5] == 'i' || htmlContent[i + 5] == 'I') &&
                        (htmlContent[i + 6] == 'p' || htmlContent[i + 6] == 'P') &&
                        (htmlContent[i + 7] == 't' || htmlContent[i + 7] == 'T') &&
                        (char.IsWhiteSpace(htmlContent[i + 8]) || htmlContent[i + 8] == '>'))
                    {
                        if (htmlContent[i + 8] != '>')
                        {
                            i += 7;
                            while (i < htmlContent.Length && htmlContent[i] != '>')
                                i++;
                        }
                        else
                            i += 8;
                        insideScriptBlock = false;
                    }
                    continue;
                }

                // style block
                if (!insideStyleBlock)
                {
                    if (i + 7 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        (htmlContent[i + 1] == 's' || htmlContent[i + 1] == 'S') &&
                        (htmlContent[i + 2] == 't' || htmlContent[i + 2] == 'T') &&
                        (htmlContent[i + 3] == 'y' || htmlContent[i + 3] == 'Y') &&
                        (htmlContent[i + 4] == 'l' || htmlContent[i + 4] == 'L') &&
                        (htmlContent[i + 5] == 'e' || htmlContent[i + 5] == 'E') &&
                        (char.IsWhiteSpace(htmlContent[i + 6]) || htmlContent[i + 6] == '>'))
                    {
                        i += 5;
                        insideStyleBlock = true;
                        continue;
                    }
                }
                else // inside script block
                {
                    if (i + 8 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        htmlContent[i + 1] == '/' &&
                        (htmlContent[i + 2] == 's' || htmlContent[i + 2] == 'S') &&
                        (htmlContent[i + 3] == 't' || htmlContent[i + 3] == 'C') &&
                        (htmlContent[i + 4] == 'y' || htmlContent[i + 4] == 'R') &&
                        (htmlContent[i + 5] == 'l' || htmlContent[i + 5] == 'I') &&
                        (htmlContent[i + 6] == 'e' || htmlContent[i + 6] == 'P') &&
                        (char.IsWhiteSpace(htmlContent[i + 7]) || htmlContent[i + 7] == '>'))
                    {
                        if (htmlContent[i + 7] != '>')
                        {
                            i += 7;
                            while (i < htmlContent.Length && htmlContent[i] != '>')
                                i++;
                        }
                        else
                            i += 7;
                        insideStyleBlock = false;
                    }
                    continue;
                }

                if (!insideTag)
                {
                    if (i < htmlContent.Length &&
                        htmlContent[i] == '<')
                    {
                        insideTag = true;
                        continue;
                    }
                }
                else // inside tag
                {
                    if (!insideAttributeValue)
                    {
                        if (htmlContent[i] == '"' || htmlContent[i] == '\'')
                        {
                            attributeValueDelimiter = htmlContent[i];
                            insideAttributeValue = true;
                            continue;
                        }
                        if (htmlContent[i] == '>')
                        {
                            insideTag = false;
                            sb.Append(' '); // prevent words from different tags (<td>sT for example) from joining together
                            continue;
                        }
                    }
                    else // inside tag and inside attribute value
                    {
                        if (htmlContent[i] == attributeValueDelimiter)
                        {
                            insideAttributeValue = false;
                            continue;
                        }
                    }
                    continue;
                }

                sb.Append(htmlContent[i]);
            }

            if (replaceNamedEntities)
                foreach (string[] htmlNamedEntity in htmlNamedEntities)
                    sb.Replace(htmlNamedEntity[0], htmlNamedEntity[1]);

            if (replaceNumberedEntities)
                for (int i = 0; i < 512; i++)
                    sb.Replace("&#" + i + ";", ((char)i).ToString());

            return sb.ToString();
        }



        public class Eratosthenes : IEnumerable<int>
        {
            //http://www.blackwasp.co.uk/PrimeFactors.aspx
            private static List<int> _primes = new List<int>();
            private int _lastChecked;

            public Eratosthenes()
            {
                _primes.Add(2);
                _lastChecked = 2;
            }
            private bool IsPrime(int checkValue)
            {
                bool isPrime = true;

                foreach (int prime in _primes)
                {
                    if ((checkValue % prime) == 0 && prime <= Math.Sqrt(checkValue))
                    {
                        isPrime = false;
                        break;
                    }
                }

                return isPrime;
            }
            public IEnumerator<int> GetEnumerator()
            {
                foreach (int prime in _primes)
                {
                    yield return prime;
                }

                while (_lastChecked < int.MaxValue)
                {
                    _lastChecked++;

                    if (IsPrime(_lastChecked))
                    {
                        _primes.Add(_lastChecked);
                        yield return _lastChecked;
                    }
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public static IEnumerable<int> GetPrimeFactors(
int value, Eratosthenes eratosthenes)
            {
                List<int> factors = new List<int>();

                foreach (int prime in eratosthenes)
                {
                    while (value % prime == 0)
                    {
                        value /= prime;
                        factors.Add(prime);
                    }

                    if (value == 1)
                    {
                        break;
                    }
                }

                return factors;
            }

            public static Dictionary<int, int> GetPrimeFactorsS(int value)
            {
                Dictionary<int, int> factors = new Dictionary<int, int>();
                Eratosthenes eratosthenesO = new Eratosthenes();
                IEnumerable<int> f = GetPrimeFactors(value, eratosthenesO);
                int currentPrime = -1;
                foreach (int i in f)
                {
                    if (currentPrime == i)
                        factors[i]++;
                    else
                    {
                        currentPrime = i;
                        factors.Add(i, 1);
                    }
                }
                return factors;

            }
        }



        /////////////////////////////////////////////////////////////////////////////////////
        /// STRING Manips
        /// 
        /////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// returns the first string inside source which is between leftstr and rightstr (excluded)
        /// the largest possible string is retrieved
        /// </summary>
        public static string GetBetween(string source, string leftstr, string rightstr)
        {
            int p1 = source.IndexOf(leftstr);
            if (p1 > -1) p1 += leftstr.Length; else return "";

            int p2 = source.LastIndexOf(rightstr);
            if (p2 < 0) p2 = source.Length;
            if (p1 >= p2) return "";
            return source.Substring(p1, p2 - p1);
        }
        /// <summary>
        /// returns the first string inside source which is between leftstr and rightstr (excluded)
        /// the largest OR smallest possible string is retrieved
        /// </summary>
        /// <param name="source"></param>
        /// <param name="leftstr"></param>
        /// <param name="rightnextstr"></param>
        /// <param name="smallest"></param>
        /// <returns></returns>
        public static string GetBetween(string source, string leftstr, string rightnextstr, bool smallest)
        {
            if (!smallest) return GetBetween(source, leftstr, rightnextstr);
            int p1 = source.IndexOf(leftstr);
            if (p1 > -1) p1 += leftstr.Length; else return "";

            int p2 = source.IndexOf(rightnextstr, p1 + 1);
            if (p2 < 0) p2 = source.Length;
            if (p1 >= p2) return "";
            return source.Substring(p1, p2 - p1);
        }

        /// <summary>
        /// returns the input string where the substring between the given limits have been removed
        /// the largest possible substring is considered
        /// </summary>
        public static string RemoveBetween(string source, string leftstr, string rightstr)
        {
            int p1 = source.IndexOf(leftstr);
            if (p1 == -1) return source;

            int p2 = source.LastIndexOf(rightstr);
            if (p2 < 0) p2 = source.Length - rightstr.Length;
            if (p1 >= p2) return source;
            return source.Remove(p1, p2 + rightstr.Length - p1);
        }
        /// <summary>
        /// returns the input string where the substring between the given limits have been removed
        /// the largest OR smallest possible substring is retrieved
        /// </summary>
        /// <param name="source"></param>
        /// <param name="leftstr"></param>
        /// <param name="rightnextstr"></param>
        /// <param name="smallest"></param>
        /// <returns></returns>
        public static string RemoveBetween(string source, string leftstr, string rightnextstr, bool smallest)
        {
            if (!smallest) return RemoveBetween(source, leftstr, rightnextstr);
            int p1 = source.IndexOf(leftstr);
            if (p1 == -1) return source;

            int p2 = source.IndexOf(rightnextstr, p1 + leftstr.Length);
            if (p2 < 0) p2 = source.Length - rightnextstr.Length;
            if (p1 + leftstr.Length >= p2) return source;
            return source.Remove(p1, p2 + rightnextstr.Length - p1);
        }


        public static string getBeginning(this string s, int n)
        {
            if (n < 1) return "";
            if (n > s.Length) return s;
            return s.Substring(0, n);
        }

        /// <summary>
        /// search for the first substring inside brackets, parenth. ... [({ and returns it if found
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string getBetw(this string s)
        {
            return s.getBetw(0);
        }
        /// <summary>
        /// search for the first substring inside brackets, parenth. ... [({ and returns it if found
        /// start searching at startidx
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string getBetw(this string s, int startidx)
        {
            int y = s.IndexOfAny(new char[] { '[', '(', '{', '<' }, startidx);
            if (y < 0) return "";
            char rights = s[y] == '[' ? ']' : (s[y] == '(' ? ')' : (s[y] == '{') ? '}' : '>');
            int r = s.IndexOf(rights, y + 1);
            if (r < 0) return "";
            return s.Substring(y + 1, r - y - 1);
        }
        public static string SimpleRegex(this string input, string regexs)
        {
            Match m = Regex.Match(input, regexs, RegexOptions.IgnoreCase);
            if (m == null) return "";
            return m.Groups[0].Value;
        }
        public static string SimpleRegex(this string input, string regex, string namedGroup)
        {
            Match m = Regex.Match(input, regex);
            if (m == null) return "";
            return m.Groups[namedGroup].Value;
        }
        public static string getv(this Match m, string groupname)
        {
            if (m.Groups[groupname] == null) return "";
            return m.Groups[groupname].Value.Trim();
        }
        public static string getv(this XmlNode m, string attrName)
        {
            if (m == null) return "";
            if (m.Attributes[attrName] == null) return "";
            return m.Attributes[attrName].Value.Trim();
        }
        public static int ENT(this double m)
        {
            return (int)Math.Floor(m);
        }
        public static int ENT(this decimal m)
        {
            return (int)Math.Floor(m);
        }

        /// <summary>
        /// RetryUtility.RetryAction( () => SomeFunctionThatCanFail(), 3, 1000 );
        /// </summary>
        /// <param name="action"></param>
        /// <param name="numRetries"></param>
        /// <param name="retryTimeToInsert">time in ms</param>
        /// <remarks>http://stackoverflow.com/questions/1563191/c-cleanest-way-to-write-retry-logic</remarks>
        public static void RetryAction3T(Action action, int retryTimeToInsert, string ErrorMsg)
        {
            RetryAction(action, 3, retryTimeToInsert, retryTimeToInsert, ErrorMsg, true);
        }
        public static void RetryAction(Action action, int numRetries, int retryTimeToInsert)
        {
            RetryAction(action, numRetries, retryTimeToInsert, retryTimeToInsert, "", true);
        }
        public static Exception RetryActionE(Action action, int numRetries, int retryTimeToInsert)
        {
            return RetryActionE(action, numRetries, retryTimeToInsert, retryTimeToInsert, "", true);
        }
        public static void RetryAction(Action action, int numRetries, int retryTimeToInsert, int retryTimeToInsertMax)
        {
            RetryAction(action, numRetries, retryTimeToInsert, retryTimeToInsertMax, "", true);
        }
        public static Exception RetryActionE(Action action, int numRetries, int retryTimeToInsert, int retryTimeToInsertMax)
        {
            return RetryActionE(action, numRetries, retryTimeToInsert, retryTimeToInsertMax, "", true);
        }
        public static void RetryAction(Action action, int numRetries, int retryTimeToInsert, int retryTimeToInsertMax, string errorMSG, bool ShowTime)
        {
            RetryAction(action, numRetries, retryTimeToInsert, retryTimeToInsertMax, errorMSG, ShowTime, false);
        }
        public static Exception RetryActionE(Action action, int numRetries, int retryTimeToInsert, int retryTimeToInsertMax, string errorMSG, bool ShowTime)
        {
            return RetryAction(action, numRetries, retryTimeToInsert, retryTimeToInsertMax, errorMSG, ShowTime, false);
        }
        public static Exception RetryAction(Action action, int numRetries, int retryTimeToInsert, int retryTimeToInsertMax, string errorMSG, bool ShowTime, bool throwExcpt)
        {
            Exception lastE;
            int maxRetries = numRetries;
            if (action == null)
                throw new ArgumentNullException("'Action' cannot be null"); // slightly safer...

            do
            {
                try { action(); return null; }
                catch (Exception E)
                {

                    Console.Write("Error ! ");
                    if (!string.IsNullOrEmpty(errorMSG)) Console.WriteLine(errorMSG);
                    Console.WriteLine(E.Message);
                    if (numRetries <= 0) throw E;  // improved to avoid silent failure
                    else
                    {
                        int wait = retryTimeToInsert + (retryTimeToInsertMax - retryTimeToInsert) * (maxRetries - numRetries) / maxRetries;
                        if (ShowTime) Console.WriteLine("waiting {0}s before retrying ", (wait) / 1000);
                        System.Threading.Thread.Sleep(1 + wait);
                    }
                    lastE = E;
                }
            } while (--numRetries > 0);
            if (throwExcpt) throw lastE;
            return lastE;
        }

        ///TAGS and more...
        ///
        /// <summary>
        /// Returns the position after the closing of the matching tag which is at given position 
        /// </summary>
        /// <param name="tagPosition">position of the "&lt;" defining the beginning of a tag.</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int getMatchingHTag(int tagPosition, string content)
        {
            if (tagPosition < 0) return -1;
            int taglimit = content.IndexOfAny(new char[] { ' ', '\t', '>', '/' }, tagPosition);
            if (taglimit < 0) return -1;
            if (content[taglimit] == '/') return taglimit; //selfclosing tag            
            //tag contains opening <
            string tag = content.Substring(tagPosition, taglimit - tagPosition);
            int nextsameTag = content.IndexOf(tag, tagPosition + 1);
            int nextclosingTag = content.IndexOf("</" + tag.Substring(1), tagPosition + 1);
            if (nextclosingTag < 0) return content.Length;
            int nextclosingTagClosing = content.IndexOf('>', nextclosingTag);
            if (nextclosingTag < 0) nextclosingTag = content.Length;
            if (nextsameTag > -1 && nextsameTag < nextclosingTag + 1)
            {
                nextclosingTag = content.IndexOf("</" + tag.Substring(1), getMatchingHTag(nextsameTag, content));
                if (nextclosingTag < 0)
                {
                    //textBox1.AppendText("BIZARRERIE : tag non fermé");
                    nextclosingTag = content.Length;
                }
                nextclosingTagClosing = content.IndexOf('>', nextclosingTag);
            }
            return nextclosingTagClosing + 1;
        }

        private static string stripTag(int startposition, string source)
        {
            if (startposition < 0) return source;
            int posl = getMatchingHTag(startposition, source);
            return source.Remove(startposition, posl - startposition);
        }
        private static string getTag(int startposition, string source)
        {
            if (startposition < 0) return "";
            int posl = getMatchingHTag(startposition, source);
            if (posl == -1) posl = source.Length;
            return source.Substring(startposition, posl - startposition);
        }
        private static string getTagAndRemove(int startposition, ref string source)
        {
            if (startposition < 0) return "";
            int posl = getMatchingHTag(startposition, source);
            string inside = source.Substring(startposition, posl - startposition);
            source = source.Remove(startposition, posl - startposition);
            return inside;
        }
        private static void testXMLwriting()
        {
            ///http://www.timvw.be/2007/01/08/generating-utf-8-with-systemxmlxmlwriter/
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = new UTF8Encoding(false);
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;

            XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("root", "http://www.timvw.be/ns");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();

            string xmlString = Encoding.UTF8.GetString(memoryStream.ToArray());
        }


        /// <summary>
        /// Displaying the build date [from StackOverflow]
        /// The most reliable method turns out to be retrieving the linker timestamp from the PE header embedded in the executable file -- some C# code (by Joe Spivey) for that from the comments to Jeff's article:
        /// </summary>
        /// <returns></returns>
        /// <see cref="http://stackoverflow.com/questions/1600962/displaying-the-build-date"/>
        public static DateTime RetrieveLinkerTimestamp(string filePath)
        {
            //GetArdepTools.log.VerboseDebugLogMethod();
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }
        public static DateTime RetrieveLinkerTimestamp()
        {
            return RetrieveLinkerTimestamp(System.Reflection.Assembly.GetCallingAssembly().Location);
        }

        public static string ShowRowData(System.Data.DataRow ROW, int FieldNameLength = 25)
        {
            string rowINfo = "";
            if (FieldNameLength < 1) FieldNameLength = 1;
            if (FieldNameLength > 80) FieldNameLength = 80;
            foreach (System.Data.DataColumn dc in ROW.Table.Columns)
            {
                //Console.WriteLine("{0,10} : {1}", dc.Caption, currentBlocToTransfert[dc.Caption]);
                try { rowINfo += string.Format(Environment.NewLine + "{0," + FieldNameLength + "} : {1}", dc.Caption, ROW[dc.Caption]); }
                catch { }
            }
            return rowINfo;
        }




        /// <summary>
        /// http://stackoverflow.com/questions/923771/quickest-way-to-convert-a-base-10-number-to-any-base-in-net    
        /// </summary>
        /// 
        public static class ConvertNumFromBaseToBase
        {

            public static readonly char[] BaseChars =
         "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            private static readonly Dictionary<char, int> CharValues = BaseChars
                       .Select((c, i) => new { Char = c, Index = i })
                       .ToDictionary(c => c.Char, c => c.Index);
            
            public static readonly char[] BaseChars36 =
                "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            public static readonly char[] BaseChars36minus =
                "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();

            public static string LongToBase(long value)
            {
                return LongToBase(value, BaseChars);
            }

            public static string LongToBase(long value, char[] alphabet)
            
            {
                long targetBase = alphabet.Length;
                // Determine exact number of characters to use.
                char[] buffer = new char[Math.Max(
                           (int)Math.Ceiling(Math.Log(value + 1, targetBase)), 1)];

                var i = buffer.Length;
                do
                {
                    buffer[--i] = alphabet[value % targetBase];
                    value = value / targetBase;
                }
                while (value > 0);

                return new string(buffer, i, buffer.Length - i);
            }

            public static long BaseToLong(string number)
            {
                char[] chrs = number.ToCharArray();
                int m = chrs.Length - 1;
                int n = BaseChars.Length, x;
                long result = 0;
                for (int i = 0; i < chrs.Length; i++)
                {
                    x = CharValues[chrs[i]];
                    result += x * (long)Math.Pow(n, m--);
                }
                return result;
            }


            // convert to binary
            static string binary = IntToString(42, new char[] { '0', '1' });

            // convert to hexadecimal
            static string hex = IntToString(42,
                new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         'A', 'B', 'C', 'D', 'E', 'F'});

            // convert to sexagesimal
            static string xx = IntToString(42,
                new char[] { '0','1','2','3','4','5','6','7','8','9',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x'});


            public static string IntToString(int value, char[] baseChars)
            {
                string result = string.Empty;
                int targetBase = baseChars.Length;

                do
                {
                    result = baseChars[value % targetBase] + result;
                    value = value / targetBase;
                }
                while (value > 0);

                return result;
            }

            /// <summary>
            /// An optimized method using an array as buffer instead of 
            /// string concatenation. This is faster for return values having 
            /// a length > 1.
            /// </summary>
            public static string IntToStringFast(int value, char[] baseChars)
            {
                // 32 is the worst cast buffer size for base 2 and int.MaxValue
                int i = 32;
                char[] buffer = new char[i];
                int targetBase = baseChars.Length;

                do
                {
                    buffer[--i] = baseChars[value % targetBase];
                    value = value / targetBase;
                }
                while (value > 0);

                char[] result = new char[32 - i];
                Array.Copy(buffer, i, result, 0, 32 - i);

                return new string(result);
            }
        }
        
           /// <summary>
        /// to create shorter GUID
        /// FROM https://jopinblog.wordpress.com/2009/02/04/a-shorter-friendlier-guiduuid-in-net/
        /// </summary>
        public class UniqueIdGenerator
        {
            private static readonly UniqueIdGenerator _instance = new UniqueIdGenerator();
            private static char[] _charMap = { // 0, 1, O, and I omitted intentionally giving 32 (2^5) symbols
                '2', '3', '4', '5', '6', '7', '8', '9', 
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
         
            public static UniqueIdGenerator GetInstance()
            {
                return _instance;
            }
         
            private RNGCryptoServiceProvider _provider = new RNGCryptoServiceProvider();
         
            private UniqueIdGenerator()
            {
            }
         
            public void GetNext(byte[] bytes)
            {
                _provider.GetBytes(bytes);
            }
         
            public string GetBase32UniqueId(int numDigits)
            {
                return GetBase32UniqueId(new byte[0], numDigits);
            }
             
            public string GetBase32UniqueId(byte[] basis, int numDigits)
            {
                int byteCount = 16;
                var randBytes = new byte[byteCount - basis.Length];
                GetNext(randBytes);
                var bytes = new byte[byteCount];
                Array.Copy(basis, 0, bytes, byteCount - basis.Length, basis.Length);
                Array.Copy(randBytes, 0, bytes, 0, randBytes.Length);
         
                ulong lo = (((ulong)BitConverter.ToUInt32(bytes, 8)) << 32) | BitConverter.ToUInt32(bytes, 12); // BitConverter.ToUInt64(bytes, 8);
                ulong hi = (((ulong)BitConverter.ToUInt32(bytes, 0)) << 32) | BitConverter.ToUInt32(bytes, 4);  // BitConverter.ToUInt64(bytes, 0);
                ulong mask = 0x1F;
         
                var chars = new char[26];
                int charIdx = 25;
         
                ulong work = lo;
                for (int i = 0; i < 26; i++)
                {
                    if (i == 12)
                    {
                        work = ((hi & 0x01) << 4) & lo;
                    }
                    else if (i == 13)
                    {
                        work = hi >> 1;
                    }
                    byte digit = (byte)(work & mask);
                    chars[charIdx] = _charMap[digit];
                    charIdx--;
                    work = work >> 5;
                }
         
                var ret = new string(chars, 26 - numDigits, numDigits);
                return ret;
            }

            public static string ticksTo2080base36()
            {
                return CoolTools.ConvertNumFromBaseToBase.LongToBase(
                    (new DateTime(2080, 01, 01).Ticks - DateTime.Now.Ticks) / 1000000,
                    ConvertNumFromBaseToBase.BaseChars36minus);
            }
            public static string ticksFrom2022base36()
            {
                return CoolTools.ConvertNumFromBaseToBase.LongToBase(
                    (DateTime.Now.Ticks- new DateTime(2022, 01, 01).Ticks ) / 1000000,
                    ConvertNumFromBaseToBase.BaseChars36minus);
            }


            public static string generateUIDdesc()
            {
                return ticksTo2080base36() + "-" + new UniqueIdGenerator().GetBase32UniqueId(6);
            }
            public static string generateUIDasc()
            {
                return ticksFrom2022base36() + "-" + new UniqueIdGenerator().GetBase32UniqueId(6);
            }
        }
    }/// class CoolTools
//////////////////////////////////////////////////////////////////////////////






    ///////////////////////////////////////////////////////////////////////////////////////////

    [Serializable()]
    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue>
        : Dictionary<TKey, TValue>, IXmlSerializable, ISerializable
    {
        public SerializableDictionary()
        {
        }

        public SerializableDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
        }

        #region ISerializable Members

        protected SerializableDictionary(SerializationInfo info, StreamingContext context)
        {
            int itemCount = info.GetInt32("ItemCount");
            for (int i = 0; i < itemCount; i++)
            {
                KeyValuePair<TKey, TValue> kvp = (KeyValuePair<TKey, TValue>)info.GetValue(String.Format("Item{0}", i), typeof(KeyValuePair<TKey, TValue>));
                this.Add(kvp.Key, kvp.Value);
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ItemCount", this.Count);
            int itemIdx = 0;
            foreach (KeyValuePair<TKey, TValue> kvp in this)
            {
                info.AddValue(String.Format("Item{0}", itemIdx), kvp, typeof(KeyValuePair<TKey, TValue>));
                itemIdx++;
            }
        }

        #endregion

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
        #endregion
    }


    public static class StringHelper
    {
        public static bool IsCapitalized(this string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            return char.IsUpper(s[0]);
        }

        public static string[] Splite(this string s, string OneStringSeparator)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(OneStringSeparator)) return new string[] { };
            if (OneStringSeparator.Length == 1)
                return s.Split(new char[] { OneStringSeparator[0] });
            return s.Split(new string[] { OneStringSeparator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the provided string. 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separatorS_inOneString">Separators are individual CHARS submitted in one string</param>
        /// <returns></returns>
        public static string[] SplitEx(this string s, string separatorS_inOneString)
        {
            return SplitEx(s, separatorS_inOneString, false);
        }
        public static string[] SplitEx(this string s, string separatorS_inOneString, bool removeEmpty)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(separatorS_inOneString)) return new string[] { };
            return s.Split(separatorS_inOneString.ToCharArray(), removeEmpty ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }


        /// <summary>
        /// Avec option StringSplitOptions.RemoveEmptyEntries
        /// </summary>
        /// <param name="s"></param>
        /// <param name="OneStringSeparator"></param>
        /// <returns></returns>
        public static string[] SplitExsre(this string s, string OneStringSeparator)
        {//REMOVE EMPTYSTRINGS
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(OneStringSeparator)) return new string[] { };
            if (OneStringSeparator.Length == 1)
                return s.Split(new char[] { s[0] });
            return s.Split(new string[] { OneStringSeparator }, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// Split la chaine selon separator1 et separator2
        /// Avec option StringSplitOptions.RemoveEmptyEntries
        /// </summary>
        /// <param name="s"></param>
        /// <param name="OneStringSeparator"></param>
        /// <returns></returns>
        public static string[] SplitExsre(this string s, string separator1, string separator2)
        {//REMOVE EMPTYSTRINGS
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(separator1)) return new string[] { };
            return s.Split(new string[] { separator1, separator2 }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int CountStringOccurrences(this string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }


        /// <summary>
        /// Join two strings with just one separating pattern.
        /// "A".joinWithOne("B") => "AB"
        /// "c:\".joinWithOne(@"\folder",@"8") => "c:\folder"
        /// "http://server/".joinWithOne("/path",@"/") => "http://server/path"
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="liant"></param>
        /// <returns></returns>
        public static string joinWithOne(this string first, string second, string liant = "")
        {
            if (liant == "") return first + second;
            string f;
            if (first.EndsWith(liant)) f = first.Substring(0, first.Length - liant.Length); else f = first;
            if (second.StartsWith(liant)) return f + second;
            return f + liant + second;
        }



    }



    namespace DoctaJonez.Drawing.Imaging
    {
        /// <summary>
        /// Provides various image untilities, such as high quality resizing and the ability to save a JPEG.
        /// </summary>
        public static class ImageUtilities
        {
            /// <summary>
            /// A quick lookup for getting image encoders
            /// </summary>
            private static Dictionary<string, ImageCodecInfo> encoders = null;

            /// <summary>
            /// A quick lookup for getting image encoders
            /// </summary>
            public static Dictionary<string, ImageCodecInfo> Encoders
            {
                //get accessor that creates the dictionary on demand
                get
                {
                    //if the quick lookup isn't initialised, initialise it
                    if (encoders == null)
                    {
                        encoders = new Dictionary<string, ImageCodecInfo>();
                    }

                    //if there are no codecs, try loading them
                    if (encoders.Count == 0)
                    {
                        //get all the codecs
                        foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                        {
                            //add each codec to the quick lookup
                            encoders.Add(codec.MimeType.ToLower(), codec);
                        }
                    }

                    //return the lookup
                    return encoders;
                }
            }

            /// <summary>
            /// Resize the image to the specified width and height.
            /// </summary>
            /// <param name="image">The image to resize.</param>
            /// <param name="width">The width to resize to.</param>
            /// <param name="height">The height to resize to.</param>
            /// <returns>The resized image.</returns>
            public static System.Drawing.Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
            {
                //a holder for the result
                Bitmap result = new Bitmap(width, height);

                //use a graphics object to draw the resized image into the bitmap
                using (Graphics graphics = Graphics.FromImage(result))
                {
                    //set the resize quality modes to high quality
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    //draw the image into the target bitmap
                    graphics.DrawImage(image, 0, 0, result.Width, result.Height);
                }

                //return the resulting bitmap
                return result;
            }

            /// <summary> 
            /// Saves an image as a jpeg image, with the given quality 
            /// </summary> 
            /// <param name="path">Path to which the image would be saved.</param> 
            /// <param name="quality">An integer from 0 to 100, with 100 being the 
            /// highest quality</param> 
            /// <exception cref="ArgumentOutOfRangeException">
            /// An invalid value was entered for image quality.
            /// </exception>
            public static void SaveJpeg(string path, Image image, int quality)
            {
                //ensure the quality is within the correct range
                if ((quality < 0) || (quality > 100))
                {
                    //create the error message
                    string error = string.Format("Jpeg image quality must be between 0 and 100, with 100 being the highest quality.  A value of {0} was specified.", quality);
                    //throw a helpful exception
                    throw new ArgumentOutOfRangeException(error);
                }

                //create an encoder parameter for the image quality
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                //get the jpeg codec
                ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

                //create a collection of all parameters that we will pass to the encoder
                EncoderParameters encoderParams = new EncoderParameters(1);
                //set the quality parameter for the codec
                encoderParams.Param[0] = qualityParam;
                //save the image using the codec and the parameters
                image.Save(path, jpegCodec, encoderParams);
            }

            /// <summary> 
            /// Returns the image codec with the given mime type 
            /// </summary> 
            public static ImageCodecInfo GetEncoderInfo(string mimeType)
            {
                //do a case insensitive search for the mime type
                string lookupKey = mimeType.ToLower();

                //the codec to return, default to null
                ImageCodecInfo foundCodec = null;

                //if we have the encoder, get it to return
                if (Encoders.ContainsKey(lookupKey))
                {
                    //pull the codec from the lookup
                    foundCodec = Encoders[lookupKey];
                }

                return foundCodec;
            }




        }
    }
    ////////////////////////////////////////////////////////////////
    /// METHODES d'EXTENSION
    /// ////////////////////////////////////////////////////////////////

    public static class MyExtensions
    {
        public static int limit(this int i, int limite)
        {
            return i > limite ? limite : i;
        }
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
        public static int TryConvertToInt(this string o, int defaultValue)
        {
            int t = defaultValue;
            int.TryParse(o.ToString(), out t);
            return t;
        }

        public static float[] convertToFloats(this int[] input)
        {
            if (input == null)
            {
                return null; // Or throw an exception - your choice
            }
            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = input[i];
            }
            return output;
        }

        public static System.Drawing.Rectangle ToRectangle(this RectangleF rf)
        {
            return new Rectangle((int)rf.X, (int)rf.Y, (int)rf.Width, (int)rf.Height);
        }
        /// <summary>
        /// Convert a RectangleF with float coord expressing % 
        /// to a Rectangle with int coord expressing per 10000
        /// </summary>
        /// <param name="rf"></param>
        /// <returns></returns>
        public static System.Drawing.Rectangle ToRectangle10k(this RectangleF rf)
        {
            return new Rectangle((int)(rf.X * 100), (int)(rf.Y * 100), (int)(rf.Width * 100), (int)(rf.Height * 100));
        }


        public static System.Drawing.RectangleF ToRectangleF(this Array a)
        {
            float[] fa = { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                if (a.Length <= i) break;
                object o = a.GetValue(i);
                if (o is float || o is int || o is double || o is Int64) fa[i] = (float)o;
                else fa[i] = o.ToString().TryConvertToFloat(0);
            }
            System.Drawing.RectangleF rf = new System.Drawing.RectangleF(fa[0], fa[1], fa[2], fa[3]);
            return rf;
        }

        public static System.Drawing.RectangleF ToRectangleF(this string s, float[] deflt)
        {
            string[] posi = s.Split(',');
            float[] posif = deflt;
            for (int k = 0; k < posi.Length; k++)
            {
                posif[k] = posi[k].TryConvertToFloat(posif[k]);
            }
            return posif.ToRectangleF();
        }

        /// <summary>
        /// Traduit le tableau, même incomplet, en un RectangleF
        /// Le tableau contient les coordonnées XY du coin en haut à gauche et les coordonnées du coin opposé.
        /// </summary>
        /// <remarks>Le tableau peut contenir des nombres ou des chaines à convertir en nombres</remarks>
        /// <param name="a"></param>
        /// <returns></returns>
        public static System.Drawing.RectangleF _FromXYRBToRectangleF(this Array a)
        {
            float[] fa = { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                if (a.Length <= i) break;
                object o = a.GetValue(i);
                if (o is float || o is int || o is double || o is Int64) fa[i] = (float)o;
                else fa[i] = o.ToString().TryConvertToFloat(0);
            }
            System.Drawing.RectangleF rf = new System.Drawing.RectangleF(fa[0], fa[1], fa[2] - fa[0], fa[3] - fa[1]);
            return rf;
        }

        /// <summary>
        /// traduit une chaine d'expressions additives séparées par une virgule
        /// en un Rectangle (coord entières). Le tableau contient les coordonnées XY du coin en haut à gauche et les coordonnées du coin opposé.
        /// 
        /// </summary>
        /// <example>"0+5,10,15 + 5,12-1" => new Rectangle (5, 10, 15, 11) </example>
        /// <remarks>fait appel directement à _FromXYRBToRectangleF</remarks>
        /// <param name="s"></param>
        /// <param name="deflt"></param>
        /// <returns></returns>
        public static System.Drawing.Rectangle _FromXYRBToRectangle(this string s, int[] deflt)
        {
            /*
            int t;  float x; x=t;
            IEnumerable a1=new List<int>();    IEnumerable        f1 = new List<float>();  f1=a1; //OK! covariance
            //IEnumerable a2=new List<int>();  IEnumerable<float> f2 = new List<float>();  f2=a2; //not OK? 
             */

            return s._FromXYRBToRectangleF(deflt.convertToFloats()).ToRectangle();
        }

        /// <summary>
        /// traduit une chaine d'expressions additives séparées par une virgule
        /// en un RectangleF. Le tableau contient les coordonnées XY du coin en haut à gauche et les coordonnées du coin opposé.
        /// 
        /// </summary>
        /// <example>"0.0f+5.0f,10.5f,15.2f + 5.0f,12.5f-0.5f" => new RectangleF (5, 10.5, 7, 12) </example>
        /// <param name="s"></param>
        /// <param name="deflt"></param>
        /// <returns></returns>
        public static System.Drawing.RectangleF _FromXYRBToRectangleF(this string s, float[] deflt)
        {
            string[] posi = s.Split(',');
            float[] posif = deflt;
            for (int k = 0; k < posi.Length; k++)
            {
                float t = 0;
                string[] ty = posi[k].Split('+');
                foreach (string y in ty)
                {
                    string[] tz = y.Split('-');
                    t += tz[0].TryConvertToFloat(0);
                    for (int zi = 1; zi < tz.Length; zi++)
                    {
                        t -= tz[zi].TryConvertToFloat(0);
                    }
                }
                posif[k] = t; // posi[k].TryConvertToFloat(posif[k]);
            }
            return posif._FromXYRBToRectangleF();
        }

        public static bool isNullR(this RectangleF R)
        {
            return (Math.Abs(R.X) < float.Epsilon && Math.Abs(R.Y) < float.Epsilon && Math.Abs(R.Width) < float.Epsilon && Math.Abs(R.Height) < float.Epsilon);
        }
        public static bool isNullR(this Rectangle R)
        {
            return (R.X == 0 && R.Y == 0 && R.Width == 0 && R.Height == 0);
        }

        public static float TryConvertToFloat(this string o, float defaultValue)
        {
            float t = defaultValue;
            System.Globalization.NumberStyles NS = System.Globalization.NumberStyles.Any;
            string s = o.ToString().Replace("f", "");
            if (!float.TryParse(s, NS, Thread.CurrentThread.CurrentCulture, out t))
                float.TryParse(s, NS, CultureInfo.GetCultureInfo(""), out t);
            return t;
        }
        /// <summary>
        /// http://www.objectreference.net/post/Enum-TryParse-Extension-Method.aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theEnum"></param>
        /// <param name="valueToParse">numeric representation in string</param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static bool TryParse<T>(this Enum theEnum, string valueToParse, out T returnValue)
        {
            returnValue = default(T);
            int intEnumValue;
            if (Int32.TryParse(valueToParse, out intEnumValue))
            {
                if (Enum.IsDefined(typeof(T), intEnumValue))
                {
                    returnValue = (T)(object)intEnumValue;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Inspired by http://www.objectreference.net/post/Enum-TryParse-Extension-Method.aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theEnum"></param>
        /// <param name="valueToParse">numeric representation in string</param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        /// <example >Enum E_foobarV { foo, bar }; E_foobarV var; var= var.TryParseDirect<E_foobarV>("toto");</example>
        public static T TryParseDirect<T>(this Enum theEnum, string strValueToParse)
        {
            T r;
            try
            {
                r = (T)Enum.Parse(typeof(T), strValueToParse, true);
            }
            catch { return default(T); }
            return r;
        }
        /// <summary>
        /// Inspired by http://www.objectreference.net/post/Enum-TryParse-Extension-Method.aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theEnum"></param>
        /// <param name="valueToParse">numeric representation in string</param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        /// <example>Enum E_foobarV { foo, bar };E_foobarV var; if (tincture.TryParseChange<E_foobarV>("foo")) { }</example>
        /// 
        /// 
        /// 
        public static bool TryParseChange<T>(this Enum theEnum, string strValueToParse)
        {

            try
            {
                theEnum = (Enum)Enum.Parse(typeof(T), strValueToParse, true);
            }
            catch { return false; }
            return true;
        }
        public static bool TryParseChange2<T>(this Enum theEnum, string strValueToParse)
        {

            try
            {
                theEnum = (Enum)Enum.Parse(typeof(T), strValueToParse, true);
            }
            catch { return false; }
            return true;
        }

        public static T TryParseToEnum<T>(this string strToParse, T defaultValue)
        {
            T r;
            try
            {
                r = (T)Enum.Parse(typeof(T), strToParse, true);
            }
            catch { return default(T); }
            return r;
        }
        public static T SelectAtRandom<T>(this List<T> list, out int index)
        {
            if (list.Count < 1) { index = -1; return default(T); }

            index = (new Random()).Next(0, list.Count);
            return list[index];

        }
        public static T SelectAtRandom<U, T>(this Dictionary<U, T> dico, out U key)
        {
            int index = (new Random()).Next(0, dico.Count);
            KeyValuePair<U, T> kvp = dico.ElementAt(index);
            key = kvp.Key;
            return kvp.Value;
        }


        public static void FromCSVstring(this System.Drawing.RectangleF rf, string s)
        {
            string[] posi = s.Split(',');
            float[] posif = { 0, 0, 0, 0 };//new float[posi.Length];
            for (int k = 0; k < posi.Length; k++)
            {
                posif[k] = posi[k].TryConvertToFloat(posif[k]);
            }
            rf = posif.ToRectangleF();
        }
        public static void FromCSVstring(this System.Drawing.RectangleF rf, string s, float[] deflt)
        {
            string[] posi = s.Split(',');
            float[] posif = deflt;
            for (int k = 0; k < posi.Length; k++)
            {
                posif[k] = posi[k].TryConvertToFloat(posif[k]);
            }
            rf = posif.ToRectangleF();
        }



        ///// <summary>
        ///// a bit stupid? , because if l == null, impossible to call method on it!
        ///// NO it WORKS Fine !!
        ///// http://stackoverflow.com/questions/847209/in-c-what-happens-when-you-call-an-extension-method-on-a-null-object
        ///// </summary>
        ///// <param name="l"></param>
        ///// <returns></returns>
        //public static bool isEmpty (this List<T> l) {
        //    return (l == null || l.Count == 0);
        //}
        //public static bool isListEmpty(List<object> l)
        //{
        //    return (l == null || l.Count == 0);
        //}


        /// <summary>
        /// Determines whether the collection is null or contains no elements.
        /// </summary>
        /// <typeparam name="T">The IEnumerable type.</typeparam>
        /// <param name="enumerable">The enumerable, which may be null or empty.</param>
        /// <returns>
        ///     <c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c>.
        /// </returns>
        /// <see cref="http://stackoverflow.com/questions/8582344/does-c-sharp-have-isnullorempty-for-list-ienumerable"/>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }
            /* If this is a list, use the Count property for efficiency. 
                * The Count property is O(1) while IEnumerable.Count() is O(N). */
            var collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count < 1;
            }
            return !enumerable.Any();
        }

        /// <summary>
        /// Let's avoid working with null collections in foreach ...
        /// </summary>
        /// <typeparam name="T">The IEnumerable type.</typeparam>
        /// <param name="enumerable">The enumerable, which may be null or empty.</param>
        /// <returns>
        ///     An empty collection if null.
        /// </returns>
        /// <see cref="http://stackoverflow.com/questions/8582344/does-c-sharp-have-isnullorempty-for-list-ienumerable"/>
        public static IEnumerable<T> MakeEmptyIfNull<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return new List<T>();
            }
            return enumerable;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
        /// shuffles the list
        /// "If you need a better quality of randomness in your shuffles use the random number generator in System.Security.Cryptography"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List to shuffle</param>
        /// <returns>Old index : if [A B C] becomes [C B A], old index is [2 1 0]</returns>
        public static List<int> ShuffleSecure<T>(this IList<T> list)
        {
            System.Security.Cryptography.RNGCryptoServiceProvider provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
            int n = list.Count;
            List<int> oldIndex = new List<int>(n);
            for (int i = 0; i < n; i++)
            {
                oldIndex.Add(i);
            }
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
                int i = oldIndex[k];
                oldIndex[k] = oldIndex[n];
                oldIndex[n] = i;
            }
            return oldIndex;
        }

        /// <summary>
        /// cancels the shuffle of a list using it's old indexes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="oldIndex">see IList.ShuffleSecure() return value</param>
        /// <returns>backup of the list before cancelling the shuffle</returns>
        public static List<T> cancelShuffleSecure<T>(this IList<T> list, List<int> oldIndex)
        {
            List<T> copy = new List<T>(list);
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = copy[oldIndex[i]];
            }
            return copy;
        }
        
     

    }
}

