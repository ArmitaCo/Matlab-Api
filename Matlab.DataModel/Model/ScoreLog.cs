using System;

namespace Matlab.DataModel
{
    public class ScoreLog
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public ScoreReason Reason { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}