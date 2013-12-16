using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class MissionFail
    {
        public int MissionFailID { get; set; }
        public int MissionID { get; set; }
        public int UserID_1 { get; set; }
        public int UserID_2 { get; set; }
    }
}