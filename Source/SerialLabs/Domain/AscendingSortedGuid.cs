using System;
using System.Globalization;

namespace SerialLabs.Data
{
    public class AscendingSortedGuid : SortedGuid
    {

        public static AscendingSortedGuid Parse(string id)
        {
            Guard.ArgumentNotNullOrEmpty(id, "id");
            if (!RegexHelper.IsSortedGuid(id)) { throw new ArgumentException("Not valid SortedGuid"); }

            AscendingSortedGuid result = new AscendingSortedGuid();

            var splits = id.Split(Separator);

            string inversedDate = splits[0];
            string guid = splits[1];
            long date = /*DateTime.MaxValue.Ticks - */long.Parse(inversedDate);

            result.Guid = Guid.Parse(guid);
            result.TimeStamp = new DateTimeOffset(date, new TimeSpan(0));

            return result;
        }

        public static AscendingSortedGuid NewSortedGuid()
        {
            AscendingSortedGuid result = new AscendingSortedGuid();

            result.Guid = Guid.NewGuid();
            result.TimeStamp = DateTimeOffset.UtcNow;

            return result;
        }

        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:D19}{1}{2}", TimeStamp.Ticks, Separator, Guid);
        }
    }
}
