using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Introdution
    {
        public int IntrodutionId { get; set; }
        public int MissionId { get; set; }
        public virtual Mission mission { get; set; }

        public int UserOriginId { get; set; }

        public int UserDestinId { get; set; }

        public int GameId { get; set; }
        public virtual Game game { get; set; }
        public bool GameResult { get; set; }

        public int StateId { get; set; }
        public virtual State state { get; set; }
    }
}