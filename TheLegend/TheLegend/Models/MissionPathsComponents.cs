using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class MissionPathsComponents
    {
        public int MissionPathsComponentsID { get; set; }
        public int MissionPathID { get; set; }
        public int OriginUserID { get; set; }
        public int DestinUserID { get; set; }

    }
}