using System.Collections.Generic;
using System.Web;

namespace Matlab.DataModel
{
    public class UserPackageMinimalViewModel
    {
        public int Id { get; set; }
        public UserPackageState UserPackageState { get; set; }
        public PackageMinimalViewModel Package { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string CoverUrl { get; set; }

        public UserPackageMinimalViewModel()
        {

        }

        public UserPackageMinimalViewModel(UserPackage userPackage)
        {
            Id = userPackage.Id;
            UserPackageState = userPackage.UserPackageState;
            Package = new PackageMinimalViewModel(userPackage.Package);
            UserName = userPackage.ApplicationUser.UserName;
            ImageUrl = userPackage.Package.AbsoluteImageUrl;
            CoverUrl = userPackage.Package.AbsoluteCoverUrl;
        }
    }

    public class AddPhotoAndExternalLinksViewModel
    {
        public List<HttpPostedFileBase> files { get; set; }
        public int id { get; set; }
        public List<string> links { get; set; }
        public List<string> titles { get; set; }
    }

    public class SetPhotoAndExternalLinksViewModel
    {
        public List<int> links { get; set; }
        public ExternalArticleState? state { get; set; }
        public int Id2 { get; set; }
    }
}