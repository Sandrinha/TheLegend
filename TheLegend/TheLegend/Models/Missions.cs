using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Missions
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int MissionID { get; set; }
        public string MissionName {get; set;}
        public int TargetUserID { get; set; }
        public int MissionStateID { get; set; }
        public int UserID { get; set; }

       // public virtual ICollection <Registo> Registo { get; set; }
    }
}