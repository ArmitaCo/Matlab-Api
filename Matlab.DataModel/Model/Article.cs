using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matlab.DataModel
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
        public virtual Question Question { get; set; }

    }
}
