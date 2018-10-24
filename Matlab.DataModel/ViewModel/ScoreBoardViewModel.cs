using System.Collections.Generic;

namespace Matlab.DataModel
{
    public class ScoreBoardViewModel
    {
        public List<ScoreBoardItemViewModel> Items { get; set; }
        public ScoreBoardItemViewModel UserItem { get; set; }
        public Challenge Challenge { get; set; }
    }
}