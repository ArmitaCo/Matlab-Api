using System;

namespace Matlab.DataModel
{
    public class BugReport
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}