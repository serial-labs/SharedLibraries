using System;

namespace SerialLabs.Data
{
    public class CompositeId
    {
        public DateTime DateUtc { get; set; }
        public Guid GuId { get; set; }


        protected static string Separator = "_";

        

    }
}
