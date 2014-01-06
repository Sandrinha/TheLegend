using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class TagRelation
    {
        public int TagRelationId {get;set;}
        public string Name {get;set;}
        public int Force { get; set; }

        public virtual ICollection<RelationShip> TagRelations { get; set; }
    }
}