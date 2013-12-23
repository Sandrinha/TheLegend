using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Humor
    {
       
            public int HumorId { get; set; }
            public string EstadoHumor { get; set; }

            public virtual ICollection<UserProfile> Users { get; set; }
        
    }
}