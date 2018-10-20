namespace Matlab.DataModel
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public AvatarImage AvatarImage { get; set; }

        public ProfileViewModel()
        {

        }

        public ProfileViewModel(ApplicationUser user)
        {
            FirstName = user.UserName.Split('@')[0];//todo: correct this
            LastName = user.UserName.Split('@')[1];//todo: correct this
            AvatarImage = user.AvatarImage;
            Email = user.Email;
            Mobile ="09363505697";
        }
    }
}