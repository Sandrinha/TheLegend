using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Mission
    {
        [Key]
        public int MissionId { get; set; }
        [ForeignKey("TargetUser")]
        public int TargetUserId{ get; set; }
        public virtual UserProfile TargetUser { get; set; }
        public bool IsComplete { get; set; }
        [ForeignKey("UserMission")]
        public int UserMissionId { get; set; }
        public virtual UserProfile UserMission { get; set; }

        public virtual ICollection<Introdution> introdutions { get; set; }
    }
}