using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLegend.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        public int count { get; set; }

        public virtual ICollection<UserProfile> User { get; set; }
    }

    public partial class EventsEntities
    {
        private IQueryable<Event> ListPublicEvents()
        {
            var query = from e in events
                        where e.PublishDate <= DateTime.Now
                              && e.Visible
                        orderby e.StartDate descending
                        select e;
            return query;
        }
        public TagCould GetTagCloud()
        {
            var TagCould = new TagCould();
            TagCould.EventsCount = ListPublicEvents().count();
            var query = from tag in Tag
                        where tag.Events.Count() > 0
                        orderby t.Title
                        select new Tag
                        {
                            tag = tag.Title,
                            Count = t.Events.Count()
                        };
            TagCould.MenuTags = query.ToList();
            return TagCould;
        }
    }
}