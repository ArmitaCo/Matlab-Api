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
    
    public partial class tblContent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblContent()
        {
            this.tblCustomerServices = new HashSet<tblCustomerService>();
            this.tblTelegramContents = new HashSet<tblTelegramContent>();
        }
    
        public int ID { get; set; }
        public int code { get; set; }
        public byte zoneID { get; set; }
        public short groupID { get; set; }
        public short classID { get; set; }
        public string context { get; set; }
        public string question { get; set; }
        public Nullable<byte> optionCount { get; set; }
        public string answer { get; set; }
    
        public virtual tblClass tblClass { get; set; }
        public virtual tblGroup tblGroup { get; set; }
        public virtual tblZone tblZone { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerService> tblCustomerServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTelegramContent> tblTelegramContents { get; set; }
    }
}
