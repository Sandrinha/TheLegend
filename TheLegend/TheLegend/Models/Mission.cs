using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Mission
    {
        [Key]
        public int MissionId { get; set; }
        public int TargetUser { get; set; }
        public bool IsComplete { get; set; }
        public int UserId { get; set; }
        public virtual UserProfile User { get; set; }

        public virtual ICollection<Introdution> introdutions { get; set; }
    }
}