using SharpRaven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Logging
{
    public class SuperLogger
    {
        public Logger Logger { get; set; }
        public RavenClient RavenClient { get; set; }

        public void Capture()
        {

        }
    }
}
