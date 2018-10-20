using System.Linq;

namespace Matlab.DataModel
{
    public class PackageMinimalViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string CoverUrl { get; set; }
        public int CategoryId { get; set; }
        public UserPackageState UserPackageState { get; set; }
        public int? UserPackageId { get; set; }

        public PackageMinimalViewModel()
        {
            
        }

        public PackageMinimalViewModel(Package package,string userId)
        {
            Id = package.Id;
            Title = package.Title;
            Description = package.Description;
            ImageUrl = package.AbsoluteImageUrl;
            CoverUrl = package.AbsoluteCoverUrl;
            CategoryId = package.CategoryId;
            var userPackage= package.UserPackages.FirstOrDefault(x => x.UserId == userId);
            UserPackageState = userPackage?.UserPackageState ?? UserPackageState.NotOwned;
            UserPackageId = userPackage?.Id;
        }
    }
}