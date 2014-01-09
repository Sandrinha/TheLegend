using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Ask
    {
        public int AskId { get; set; }

        [ForeignKey("UserAsk")]
        public int UserAskId { get; set; }
        public virtual UserProfile UserAsk { get; set; }

        [ForeignKey("UserOrigin")]
        public int UserOriginId { get; set; }
        public virtual UserProfile UserOrigin { get; set; }

        [ForeignKey("UserDestin")]
        public int UserDestinId { get; set; }
        public virtual UserProfile UserDestin { get; set; }
    }
}