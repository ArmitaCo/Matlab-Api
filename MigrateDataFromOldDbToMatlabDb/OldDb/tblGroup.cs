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
    
    public partial class tblGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblGroup()
        {
            this.tblClasses = new HashSet<tblClass>();
            this.tblContents = new HashSet<tblContent>();
            this.tblCustomerServices = new HashSet<tblCustomerService>();
        }
    
        public short ID { get; set; }
        public int code { get; set; }
        public string title { get; set; }
        public byte zoneID { get; set; }
        public byte statusID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblClass> tblClasses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblContent> tblContents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerService> tblCustomerServices { get; set; }
        public virtual tblStatu tblStatu { get; set; }
        public virtual tblZone tblZone { get; set; }
    }
}