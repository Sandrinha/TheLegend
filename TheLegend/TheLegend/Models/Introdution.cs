using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Introdution
    {
        public int IntrodutionId { get; set; }
        public int MissionId { get; set; }
        public virtual Mission mission { get; set; }
        [ForeignKey("UserOrigin")]
        public int UserOriginId { get; set; }
        public virtual UserProfile UserOrigin { get; set;}
        [ForeignKey("UserDestin")]
        public int UserDestinId { get; set; }
        public virtual UserProfile UserDestin { get; set; }

        public int GameId { get; set; }
        public virtual Game game { get; set; }
        public bool GameResult { get; set; }

        public int StateId { get; set; }
        public virtual State state { get; set; }
    }
}