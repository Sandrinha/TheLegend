using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Introductions
    {
        public int IntrodutionID { get; set; }
        public int MissionID { get; set; }
        public int OrigenUserID { get; set; }
        public int DestinUserID { get; set; }
        public string StateID {get; set;}
        public int GameID { get; set; }
        public Boolean GameResult { get; set; }

    }
}