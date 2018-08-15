using System.Collections.Generic;

namespace Matlab.DataModel
{
    public class Package
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public virtual Catergory Catergory { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}