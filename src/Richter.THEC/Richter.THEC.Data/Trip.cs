using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Richter.THEC.Data
{
    public class Trip
    {
        public string Provider { get; set; }
        public int StartLocation { get; set; }
        public int EndLocation { get; set; }
        public double Distance { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Total { get; set; }
    }
}
