using System;

namespace SerialLabs.Data
{
    public class CompositeIdAsc : CompositeId
    {
        
        public override string ToString()
        {
            return String.Format("{0:D19}", /*DateTime.MaxValue.Ticks -*/ DateUtc.Ticks) + Separator + GuId;
        }

        public static CompositeIdAsc Parse(string id)
        {
            Guard.ArgumentNotNullOrEmpty(id, "id");
            if (!id.Contains(Separator))
            { throw new ArgumentException("Not valid Composite Id"); }

            CompositeIdAsc result = new CompositeIdAsc();

            var splits = id.Split(Separator.ToCharArray());

            string inversedDate = splits[0];
            string guid = splits[1];
            long date = /*DateTime.MaxValue.Ticks - */long.Parse(inversedDate);

            result.GuId = Guid.Parse(guid);
            result.DateUtc = new DateTime(date);

            return result;
        }

        public static CompositeIdAsc NewCompositeId()
        {
            CompositeIdAsc result = new CompositeIdAsc();

            result.GuId = Guid.NewGuid();
            result.DateUtc = DateTime.UtcNow;

            return result;
        }
    }
}
