using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Ask
    {
        public int AskID { get; set; }
       //public int UserId { get; set; }
        public virtual UserProfile UserOrigin { get; set; }
        public virtual UserProfile UserDestiny { get; set; }
        public virtual UserProfile UserAsk { get; set; }

    }
}