using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Birth { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection <Gamers> Registo { get; set; }
    }
}