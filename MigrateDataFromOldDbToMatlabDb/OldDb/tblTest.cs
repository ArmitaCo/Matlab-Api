//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrateDataFromOldDbToMatlabDb.OldDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblTest()
        {
            this.tblQuestions = new HashSet<tblQuestion>();
        }
    
        public short ID { get; set; }
        public int code { get; set; }
        public string title { get; set; }
        public byte zoneID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblQuestion> tblQuestions { get; set; }
        public virtual tblZone tblZone { get; set; }
    }
}