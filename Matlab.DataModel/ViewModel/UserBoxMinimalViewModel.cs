using System.Linq;

namespace Matlab.DataModel
{
    public class UserBoxMinimalViewModel
    {
        public int Id { get; set; }
        public int? UserPackageBoxId { get; set; }
        public int PackageId { get; set; }
        public int Code { get; set; }
        public BoxState BoxState { get; set; }
        public int StateValue { get; set; }
        public string Title { get; set; }

        public UserBoxMinimalViewModel()
        {
            
        }

        public UserBoxMinimalViewModel(Box box,string userId)
        {
            Id = box.Id;
            PackageId = box.PackageId;
            Code = box.Code;
            Title = box.Title;
            var userPackageBox = box.UserPackageBoxes.FirstOrDefault(x => x.UserPackage.UserId == userId);
            if (userPackageBox==null)
            {
                BoxState = BoxState.NotOwned;
                StateValue = 0;
            }
            else
            {
                BoxState = userPackageBox.State;
                StateValue = userPackageBox.StateValue;
                UserPackageBoxId = userPackageBox.Id;
            }
        }
    }
}