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
    
    public partial class tblCustomerAnswer
    {
        public int ID { get; set; }
        public int customerServiceID { get; set; }
        public int customerID { get; set; }
        public Nullable<int> questionID { get; set; }
        public Nullable<int> contentQuestionID { get; set; }
        public string answer { get; set; }
        public System.DateTime answerDate { get; set; }
    
        public virtual tblCustomer tblCustomer { get; set; }
        public virtual tblCustomerService tblCustomerService { get; set; }
        public virtual tblQuestion tblQuestion { get; set; }
    }
}
