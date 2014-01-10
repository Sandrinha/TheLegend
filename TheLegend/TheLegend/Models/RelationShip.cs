using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class RelationShip
    {
        public int RelationShipId { get; set; }
        [ForeignKey("User1")]
        public int UserId1 { get; set; }
        public virtual UserProfile User1 { get; set; }
        [ForeignKey("User2")]
        public int UserId2 { get; set; }
        public virtual UserProfile User2 { get; set; }
        public int TagRelationId { get; set; }
        public virtual TagRelation Tag { get; set; }

    }
}