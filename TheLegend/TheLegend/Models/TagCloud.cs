using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class TagCloud
    {
        public int EventsCount;
        public List<MenuTag> MenuTags = new List<MenuTag>();

        public int GetRankForTag(MenuTag tag)
        {
            if (EventsCount == 0)
                return 1;

            var result = (tag.Count * 100) / EventsCount;
            if (result <= 1)
                return 1;
            if (result <= 4)
                return 2;
            if (result <= 8)
                return 3;
            if (result <= 12)
                return 4;
            if (result <= 18)
                return 5;
            if (result <= 30)
                return 6;
            return result <= 50 ? 7 : 8;
        }
    }
}