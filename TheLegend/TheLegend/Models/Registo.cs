using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public enum Grade
    { A,B,C,D,F}


    public class Registo
    {
        public int RegistoID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Birth { get; set; }
        public string Sex { get; set; }
        public string Password { get; set; }


       
    }
}