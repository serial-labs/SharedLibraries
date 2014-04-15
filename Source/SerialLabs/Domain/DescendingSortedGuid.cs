using System;

namespace SerialLabs.Data
{
    /// <summary>
    /// A sorted guid with a lexicographic order based on its inverted timestamp value.
    /// </summary>
    public class DescendingSortedGuid : SortedGuid
    {
        /// <summary>
        /// Parse a string value into a <see cref="DescendingSortedGuid"/>
        /// </summary>
        /// <param name="id">Should be in the form of 0000000000000000000_00000000000000000000000000000000</param>
        /// <returns></returns>
        public static DescendingSortedGuid Parse(string id)
        {
            Guard.ArgumentNotNullOrWhiteSpace(id, "id");
            if (!RegexHelper.IsSortedGuid(id)) { throw new ArgumentException("Not valid SortedGuid"); }

            DescendingSortedGuid result = new DescendingSortedGuid();

            var splits = id.Split(Separator);

            string inversedDate = splits[0];
            string guid = splits[1];
            long date = DateTime.MaxValue.Ticks - long.Parse(inversedDate);

            result.Guid = Guid.Parse(guid);
            result.Timestamp = new DateTimeOffset(date, new TimeSpan(0));

            return result;
        }

        /// <summary>
        /// Try to parse a string value into a <see cref="DescendingSortedGuid"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns>Returns true if parsing succeeded, otherwise false</returns>
        public static bool TryParse(string id, out DescendingSortedGuid result)
        {
            Guard.ArgumentNotNullOrWhiteSpace(id, "id");

            result = DescendingSortedGuid.NewSortedGuid();
            try
            {
                result = DescendingSortedGuid.Parse(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new empty sorted guid
        /// </summary>
        /// <returns></returns>
        public static DescendingSortedGuid NewSortedGuid()
        {
            DescendingSortedGuid result = new DescendingSortedGuid();

            result.Guid = Guid.NewGuid();
            result.Timestamp = DateTime.UtcNow;

            return result;
        }

        /// <summary>
        /// Returns the string representation of this sorted guid
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0:D19}{1}{2:N}", DateTime.MaxValue.Ticks - Timestamp.Ticks, Separator, Guid);
        }
    }
}
