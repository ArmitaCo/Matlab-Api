using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Matlab.DataModel
{
    public partial class MatlabDb : IdentityDbContext<ApplicationUser>
    {
        //public virtual DbSet<VisitRecord> VisitRecords { get; set; }
        public virtual DbSet<Catergory> Catergories { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<UserPackage> UserPackages { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<ImageSuggest> ImageSuggests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<ExternalArticle> ExternalArticles { get; set; }
        public virtual DbSet<Box> Boxes { get; set; }
        public virtual DbSet<UserPackageBox> UserPackageBoxes { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }
        public virtual DbSet<AvatarImage> AvatarImages { get; set; }
        public virtual DbSet<ScoreLog> ScoreLogs { get; set; }
        public virtual DbSet<Challenge> Challenges { get; set; }
        public virtual DbSet<BugReport> BugReports { get; set; }

        #region Overrides of IdentityDbContext<ApplicationUser,IdentityRole,string,IdentityUserLogin,IdentityUserRole,IdentityUserClaim>

        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Catergory>()
                .HasMany(x => x.Packages)
                .WithRequired(x => x.Catergory)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<Package>()
                .HasMany(x => x.UserPackages)
                .WithRequired(x => x.Package)
                .HasForeignKey(x => x.PackageId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.UserPackages)
                .WithRequired(x => x.ApplicationUser)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Favorites)
                .WithRequired(x => x.ApplicationUser)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Article>()
                .HasMany(x => x.Favorites)
                .WithRequired(x => x.Article)
                .HasForeignKey(x => x.ArticleId);

            modelBuilder.Entity<Article>()
                .HasMany(x => x.ImageSuggests)
                .WithRequired(x => x.Article)
                .HasForeignKey(x => x.ArticleId);

            modelBuilder.Entity<Package>()
                .HasMany(x => x.Boxes)
                .WithRequired(x => x.Package)
                .HasForeignKey(x => x.PackageId);

            modelBuilder.Entity<Question>()
                .HasMany(x => x.Answers)
                .WithRequired(x => x.Question)
                .HasForeignKey(x => x.QuestionId);

            //modelBuilder.Entity<Question>()
            //    .HasRequired(x=>x.CorrectAnswer)
            //    .WithOptional(x=>x.)
                

            modelBuilder.Entity<Package>()
                .HasMany(x => x.Transactions)
                .WithRequired(x => x.Package)
                .HasForeignKey(x => x.PackageId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Transactions)
                .WithRequired(x => x.ApplicationUser)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Article>()
                .HasMany(x => x.Questions)
                .WithRequired(x => x.Article)
                .HasForeignKey(x => x.ArticleId);

            modelBuilder.Entity<Article>()
                .HasMany(x => x.ExternalArticles)
                .WithRequired(x => x.Article)
                .HasForeignKey(x => x.ArticleId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ExternalArticles)
                .WithOptional(x => x.SuggestedByUser)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserPackage>()
                .HasMany(x => x.UserPackageBoxes)
                .WithRequired(x => x.UserPackage)
                .HasForeignKey(x => x.UserPackageId);

            modelBuilder.Entity<Box>()
                .HasMany(x => x.UserPackageBoxes)
                .WithRequired(x => x.Box)
                .HasForeignKey(x => x.BoxId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Box>()
                .HasMany(x => x.Articles)
                .WithRequired(x => x.Box)
                .HasForeignKey(x => x.BoxId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.UserAnswers)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Answer>()
                .HasMany(x => x.UserAnswers)
                .WithRequired(x => x.Answer)
                .HasForeignKey(x => x.AnswerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(x => x.UserAnswers)
                .WithRequired(x => x.Question)
                .HasForeignKey(x => x.QuestionId);

            modelBuilder.Entity<Article>()
                .HasMany(x => x.Questions)
                .WithRequired(x => x.Article)
                .HasForeignKey(x => x.ArticleId);

            modelBuilder.Entity<AvatarImage>()
                .HasMany(x => x.Users)
                .WithOptional(x => x.AvatarImage)
                .HasForeignKey(x => x.AvatarImageId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ScoreLogs)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.BugReports)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);
        }

        #endregion

        public MatlabDb()
            : base("name=MatlabDb", throwIfV1Schema: false)
        {
        }

        public static MatlabDb Create()
        {
            return new MatlabDb();
        }
    }
}