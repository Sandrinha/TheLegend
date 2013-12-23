using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Ask
    {
        public int AskId { get; set; }
        public int UserAskId { get; set; }

        public int UserOriginId { get; set; }

        public int UserDestinId { get; set; }
    }
}