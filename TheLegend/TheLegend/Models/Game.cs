﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Introdution> introdutions { get; set; }

    }
}