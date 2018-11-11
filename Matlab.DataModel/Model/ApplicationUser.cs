using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Matlab.DataModel
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
			// Add custom user claims here
			return userIdentity;
		}

		public ApplicationUser()
		{
			ScoreLogs = new List<ScoreLog>();
		}

		[DefaultValue(0)]
		public int Score { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int? AvatarImageId { get; set; }
		public virtual AvatarImage AvatarImage { get; set; }
		public virtual ICollection<UserPackage> UserPackages { get; set; }
		public virtual ICollection<Favorite> Favorites { get; set; }
		public virtual ICollection<Transaction> Transactions { get; set; }
		public virtual ICollection<ExternalArticle> ExternalArticles { get; set; }
		public virtual ICollection<UserAnswer> UserAnswers { get; set; }
		public virtual ICollection<ScoreLog> ScoreLogs { get; set; }
		public virtual ICollection<BugReport> BugReports { get; set; }
	}
}