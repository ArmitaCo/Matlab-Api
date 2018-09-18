using System.Linq;

namespace Matlab.DataModel
{
    public class UserBoxMinimalViewModel
    {
        public int Id { get; set; }
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
            var upb = box.UserPackageBoxes.FirstOrDefault(x => x.UserPackage.UserId == userId);
            if (upb==null)
            {
                BoxState = BoxState.NotOwned;
                StateValue = 0;
            }
            else
            {
                BoxState = upb.State;
                StateValue = upb.StateValue;
            }
        }
    }
}