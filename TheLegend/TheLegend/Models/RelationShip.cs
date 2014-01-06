using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class RelationShip
    {
        public int RelationShipId { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public int TagRelationId { get; set; }
        public virtual TagRelation Tag { get; set; }

    }
}