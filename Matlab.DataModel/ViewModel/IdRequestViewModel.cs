namespace Matlab.DataModel
{
    public class IdRequestViewModel
    {
        public int Id { get; set; }
    }

    public class UserPackageMinimalViewModel
    {
        public int Id { get; set; }
        public UserPackageState UserPackageState { get; set; }
        public PackageMinimalViewModel Package { get; set; }
        public string UserName { get; set; }

        public UserPackageMinimalViewModel()
        {
            
        }

        public UserPackageMinimalViewModel(UserPackage userPackage)
        {
            Id = userPackage.Id;
            UserPackageState = userPackage.UserPackageState;
            Package = new PackageMinimalViewModel(userPackage.Package);
            UserName = userPackage.ApplicationUser.UserName;
        }
    }
}