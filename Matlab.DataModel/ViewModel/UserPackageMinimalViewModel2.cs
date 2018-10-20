namespace Matlab.DataModel
{
    public class UserPackageMinimalViewModel2
    {
        public int UserPackageId { get; set; }
        public UserPackageState UserPackageState { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string CoverUrl { get; set; }
        public int CategoryId { get; set; }

        public UserPackageMinimalViewModel2()
        {

        }

        public UserPackageMinimalViewModel2(UserPackage userPackage)
        {
            UserPackageId = userPackage.Id;
            UserPackageState = userPackage.UserPackageState;
            //Package = new PackageMinimalViewModel(userPackage.Package);
            Id = userPackage.Package.Id;
            Title = userPackage.Package.Title;
            Description = userPackage.Package.Description;
            ImageUrl = userPackage.Package.AbsoluteImageUrl;
            CoverUrl = userPackage.Package.AbsoluteCoverUrl;
            CategoryId = userPackage.Package.CategoryId;
        }
    }
}