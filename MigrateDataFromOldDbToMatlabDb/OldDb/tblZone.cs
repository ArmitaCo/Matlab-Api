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
    
    public partial class tblZone
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblZone()
        {
            this.tblClasses = new HashSet<tblClass>();
            this.tblContents = new HashSet<tblContent>();
            this.tblCustomerServices = new HashSet<tblCustomerService>();
            this.tblGroups = new HashSet<tblGroup>();
            this.tblTelegramContents = new HashSet<tblTelegramContent>();
            this.tblTelegramCustomerServices = new HashSet<tblTelegramCustomerService>();
            this.tblTests = new HashSet<tblTest>();
        }
    
        public byte ID { get; set; }
        public int code { get; set; }
        public string title { get; set; }
        public byte statusID { get; set; }
        public byte correctScore { get; set; }
        public byte wrongScore { get; set; }
        public int serviceNo { get; set; }
        public long serviceID { get; set; }
        public long freeServiceID { get; set; }
        public int cost { get; set; }
        public string structureMessage { get; set; }
        public string welcomeMessage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblClass> tblClasses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblContent> tblContents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerService> tblCustomerServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblGroup> tblGroups { get; set; }
        public virtual tblStatu tblStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTelegramContent> tblTelegramContents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTelegramCustomerService> tblTelegramCustomerServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTest> tblTests { get; set; }
    }
}
