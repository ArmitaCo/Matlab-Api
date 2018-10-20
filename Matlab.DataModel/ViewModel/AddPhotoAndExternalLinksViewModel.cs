using System.Collections.Generic;
using System.Web;

namespace Matlab.DataModel
{
    public class AddPhotoAndExternalLinksViewModel
    {
        public List<HttpPostedFileBase> files { get; set; }
        public int id { get; set; }
        public List<string> links { get; set; }
        public List<string> titles { get; set; }
    }
}