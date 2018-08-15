using System;

namespace Matlab.DataModel
{
    public class Transaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int Fee { get; set; }
        public string ReferenceKey { get; set; }
        public int PackageId { get; set; }
        public PayReson PayReson { get; set; }
        public virtual Package Package { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}