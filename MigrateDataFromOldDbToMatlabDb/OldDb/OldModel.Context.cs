﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HealthMessageEntities : DbContext
    {
        public HealthMessageEntities()
            : base("name=HealthMessageEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<accRole> accRoles { get; set; }
        public virtual DbSet<accUser> accUsers { get; set; }
        public virtual DbSet<accUserRole> accUserRoles { get; set; }
        public virtual DbSet<errMessage> errMessages { get; set; }
        public virtual DbSet<schScheduleJob> schScheduleJobs { get; set; }
        public virtual DbSet<schScheduleLog> schScheduleLogs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tblAdvertising> tblAdvertisings { get; set; }
        public virtual DbSet<tblClass> tblClasses { get; set; }
        public virtual DbSet<tblContent> tblContents { get; set; }
        public virtual DbSet<tblCustomer> tblCustomers { get; set; }
        public virtual DbSet<tblCustomerAnswer> tblCustomerAnswers { get; set; }
        public virtual DbSet<tblCustomerService> tblCustomerServices { get; set; }
        public virtual DbSet<tblGroup> tblGroups { get; set; }
        public virtual DbSet<tblMessage> tblMessages { get; set; }
        public virtual DbSet<tblMobileOperator> tblMobileOperators { get; set; }
        public virtual DbSet<tblQuestion> tblQuestions { get; set; }
        public virtual DbSet<tblQuestionOption> tblQuestionOptions { get; set; }
        public virtual DbSet<tblServiceStatu> tblServiceStatus { get; set; }
        public virtual DbSet<tblStatu> tblStatus { get; set; }
        public virtual DbSet<tblTelegramContent> tblTelegramContents { get; set; }
        public virtual DbSet<tblTelegramCustomerService> tblTelegramCustomerServices { get; set; }
        public virtual DbSet<tblTest> tblTests { get; set; }
        public virtual DbSet<tblZone> tblZones { get; set; }
        public virtual DbSet<vwContent> vwContents { get; set; }
        public virtual DbSet<vwCustomerScore> vwCustomerScores { get; set; }
    }
}
