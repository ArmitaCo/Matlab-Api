using System.Collections.Generic;

namespace Matlab.DataModel
{
    public class SetPhotoAndExternalLinksViewModel
    {
        public List<int> links { get; set; }
        public ExternalArticleState? state { get; set; }
        public int Id2 { get; set; }
    }
}