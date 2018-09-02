namespace Matlab.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPackages", "UserPackageState", c => c.Int(nullable: false));
            DropColumn("dbo.UserPackages", "UsePackageState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPackages", "UsePackageState", c => c.Int(nullable: false));
            DropColumn("dbo.UserPackages", "UserPackageState");
        }
    }
}
