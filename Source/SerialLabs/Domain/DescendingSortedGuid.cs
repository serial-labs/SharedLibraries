using System;

namespace SerialLabs.Data
{
    public class DescendingSortedGuid : SortedGuid
    {
        public static DescendingSortedGuid Parse(string id)
        {
            Guard.ArgumentNotNullOrEmpty(id, "id");
            if (!RegexHelper.IsSortedGuid(id)) { throw new ArgumentException("Not valid SortedGuid"); }

            DescendingSortedGuid result = new DescendingSortedGuid();

            var splits = id.Split(Separator);

            string inversedDate = splits[0];
            string guid = splits[1];
            long date = DateTime.MaxValue.Ticks - long.Parse(inversedDate);

            result.Guid = Guid.Parse(guid);
            result.TimeStamp = new DateTimeOffset(date, new TimeSpan(0));

            return result;
        }

        public static DescendingSortedGuid NewSortedGuid()
        {
            DescendingSortedGuid result = new DescendingSortedGuid();

            result.Guid = Guid.NewGuid();
            result.TimeStamp = DateTime.UtcNow;

            return result;
        }

        public override string ToString()
        {
            return String.Format("{0:D19}{1}{2}", DateTime.MaxValue.Ticks - TimeStamp.Ticks, Separator, Guid);
        }
    }
}
