namespace Matlab.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ChoiceLable = c.Int(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AnswersString = c.String(),
                        ArticleId = c.Int(nullable: false),
                        AnswersCount = c.Int(nullable: false),
                        CorrectChoiceLable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            //CreateTable(
            //    "dbo.Articles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            ImageUrl = c.String(),
            //            Order = c.Int(nullable: false),
            //            BoxId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Boxes", t => t.BoxId, cascadeDelete: true)
            //    .Index(t => t.BoxId);
            
            //CreateTable(
            //    "dbo.Boxes",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            PackageId = c.Int(nullable: false),
            //            Code = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
            //    .Index(t => t.PackageId);
            
            //CreateTable(
            //    "dbo.Packages",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            Description = c.String(),
            //            ImageUrl = c.String(),
            //            CoverUrl = c.String(),
            //            CategoryId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Catergories", t => t.CategoryId, cascadeDelete: true)
            //    .Index(t => t.CategoryId);
            
            //CreateTable(
            //    "dbo.Catergories",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            ImageUrl = c.String(),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Transactions",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            DateTime = c.DateTime(nullable: false),
            //            Fee = c.Int(nullable: false),
            //            ReferenceKey = c.String(),
            //            PackageId = c.Int(nullable: false),
            //            PayReson = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.PackageId);
            
            //CreateTable(
            //    "dbo.AspNetUsers",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Email = c.String(maxLength: 256),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(),
            //            SecurityStamp = c.String(),
            //            PhoneNumber = c.String(),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            //CreateTable(
            //    "dbo.AspNetUserClaims",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            ClaimType = c.String(),
            //            ClaimValue = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.ExternalArticles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Url = c.String(),
            //            Title = c.String(),
            //            ArticleId = c.Int(nullable: false),
            //            State = c.Int(nullable: false),
            //            UserId = c.String(maxLength: 128),
            //            CreateDateTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId)
            //    .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
            //    .Index(t => t.ArticleId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.Favorites",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            DateTime = c.DateTime(nullable: false),
            //            ArticleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.ArticleId);
            
            //CreateTable(
            //    "dbo.AspNetUserLogins",
            //    c => new
            //        {
            //            LoginProvider = c.String(nullable: false, maxLength: 128),
            //            ProviderKey = c.String(nullable: false, maxLength: 128),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.AspNetUserRoles",
            //    c => new
            //        {
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            RoleId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.UserId, t.RoleId })
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.RoleId);
            
            //CreateTable(
            //    "dbo.UserPackages",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            PackageId = c.Int(nullable: false),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            UserPackageState = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
            //    .Index(t => t.PackageId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.UserPackageBoxes",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserPackageId = c.Int(nullable: false),
            //            BoxId = c.Int(nullable: false),
            //            State = c.Int(nullable: false),
            //            StateValue = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.UserPackages", t => t.UserPackageId, cascadeDelete: true)
            //    .ForeignKey("dbo.Boxes", t => t.BoxId)
            //    .Index(t => t.UserPackageId)
            //    .Index(t => t.BoxId);
            
            //CreateTable(
            //    "dbo.ImageSuggests",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ArticleId = c.Int(nullable: false),
            //            ImageUrl = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
            //    .Index(t => t.ArticleId);
            
            //CreateTable(
            //    "dbo.AspNetRoles",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            //DropForeignKey("dbo.Questions", "ArticleId", "dbo.Articles");
            //DropForeignKey("dbo.ImageSuggests", "ArticleId", "dbo.Articles");
            //DropForeignKey("dbo.Favorites", "ArticleId", "dbo.Articles");
            //DropForeignKey("dbo.ExternalArticles", "ArticleId", "dbo.Articles");
            //DropForeignKey("dbo.UserPackageBoxes", "BoxId", "dbo.Boxes");
            //DropForeignKey("dbo.UserPackages", "PackageId", "dbo.Packages");
            //DropForeignKey("dbo.Transactions", "PackageId", "dbo.Packages");
            //DropForeignKey("dbo.UserPackages", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.UserPackageBoxes", "UserPackageId", "dbo.UserPackages");
            //DropForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.Favorites", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.ExternalArticles", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.Packages", "CategoryId", "dbo.Catergories");
            //DropForeignKey("dbo.Boxes", "PackageId", "dbo.Packages");
            //DropForeignKey("dbo.Articles", "BoxId", "dbo.Boxes");
            //DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            //DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            //DropIndex("dbo.ImageSuggests", new[] { "ArticleId" });
            //DropIndex("dbo.UserPackageBoxes", new[] { "BoxId" });
            //DropIndex("dbo.UserPackageBoxes", new[] { "UserPackageId" });
            //DropIndex("dbo.UserPackages", new[] { "UserId" });
            //DropIndex("dbo.UserPackages", new[] { "PackageId" });
            //DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            //DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            //DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            //DropIndex("dbo.Favorites", new[] { "ArticleId" });
            //DropIndex("dbo.Favorites", new[] { "UserId" });
            //DropIndex("dbo.ExternalArticles", new[] { "UserId" });
            //DropIndex("dbo.ExternalArticles", new[] { "ArticleId" });
            //DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            //DropIndex("dbo.AspNetUsers", "UserNameIndex");
            //DropIndex("dbo.Transactions", new[] { "PackageId" });
            //DropIndex("dbo.Transactions", new[] { "UserId" });
            //DropIndex("dbo.Packages", new[] { "CategoryId" });
            //DropIndex("dbo.Boxes", new[] { "PackageId" });
            //DropIndex("dbo.Articles", new[] { "BoxId" });
            //DropIndex("dbo.Questions", new[] { "ArticleId" });
            //DropIndex("dbo.Answers", new[] { "QuestionId" });
            //DropTable("dbo.AspNetRoles");
            //DropTable("dbo.ImageSuggests");
            //DropTable("dbo.UserPackageBoxes");
            //DropTable("dbo.UserPackages");
            //DropTable("dbo.AspNetUserRoles");
            //DropTable("dbo.AspNetUserLogins");
            //DropTable("dbo.Favorites");
            //DropTable("dbo.ExternalArticles");
            //DropTable("dbo.AspNetUserClaims");
            //DropTable("dbo.AspNetUsers");
            //DropTable("dbo.Transactions");
            //DropTable("dbo.Catergories");
            //DropTable("dbo.Packages");
            //DropTable("dbo.Boxes");
            //DropTable("dbo.Articles");
            //DropTable("dbo.Questions");
            //DropTable("dbo.Answers");
        }
    }
}
