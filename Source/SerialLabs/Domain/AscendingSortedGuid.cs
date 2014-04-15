using System;
using System.Globalization;

namespace SerialLabs.Data
{
    /// <summary>
    /// A sorted guid with a lexicographic order based on its natural timestamp value.
    /// </summary>
    public class AscendingSortedGuid : SortedGuid
    {
        /// <summary>
        /// Parse a string value into an <see cref="AscendingSortedGuid"/>
        /// </summary>
        /// <param name="id">Should be in the form of 0000000000000000000_00000000000000000000000000000000</param>
        /// <returns></returns>

        public static AscendingSortedGuid Parse(string id)
        {
            Guard.ArgumentNotNullOrEmpty(id, "id");
            if (!RegexHelper.IsSortedGuid(id)) { throw new ArgumentException("Not valid SortedGuid"); }

            AscendingSortedGuid result = new AscendingSortedGuid();

            var splits = id.Split(Separator);

            string inversedDate = splits[0];
            string guid = splits[1];
            long date = long.Parse(inversedDate);

            result.Guid = Guid.Parse(guid);
            result.Timestamp = new DateTimeOffset(date, new TimeSpan(0));

            return result;
        }

        /// <summary>
        /// Creates a new <see cref="AscendingSortedGuid"/>
        /// </summary>
        /// <returns></returns>
        public static AscendingSortedGuid NewSortedGuid()
        {
            AscendingSortedGuid result = new AscendingSortedGuid();

            result.Guid = Guid.NewGuid();
            result.Timestamp = DateTimeOffset.UtcNow;

            return result;
        }

        /// <summary>
        /// Try to parse a string value into a <see cref="AscendingSortedGuid"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns>Returns true if parsing succeeded, otherwise false</returns>
        public static bool TryParse(string id, out AscendingSortedGuid result)
        {
            Guard.ArgumentNotNullOrWhiteSpace(id, "id");

            result = AscendingSortedGuid.NewSortedGuid();
            try
            {
                result = AscendingSortedGuid.Parse(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the string representation of the <see cref="AscendingSortedGuid"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:D19}{1}{2:N}", Timestamp.Ticks, Separator, Guid);
        }
    }
}
