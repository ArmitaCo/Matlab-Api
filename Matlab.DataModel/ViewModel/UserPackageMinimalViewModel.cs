namespace Matlab.DataModel
{
    public class UserPackageMinimalViewModel
    {
        public int Id { get; set; }
        public UserPackageState UserPackageState { get; set; }
        public PackageMinimalViewModel Package { get; set; }
        public string UserName { get; set; }

        public UserPackageMinimalViewModel()
        {

        }

        public UserPackageMinimalViewModel(UserPackage userPackage,string userId)
        {
            Id = userPackage.Id;
            UserPackageState = userPackage.UserPackageState;
            Package = new PackageMinimalViewModel(userPackage.Package,userId);
            UserName = userPackage.ApplicationUser.UserName;
        }
    }
}