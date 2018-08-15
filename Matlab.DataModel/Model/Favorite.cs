using System;
using System.Collections.Generic;

namespace Matlab.DataModel
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int ArticleId { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}