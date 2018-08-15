namespace Matlab.DataModel
{
    public class UserPackage
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string UserId { get; set; }
        public UsePackageState UsePackageState { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Package Package { get; set; }
    }
}