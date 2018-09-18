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

        public PackageMinimalViewModel()
        {
            
        }

        public PackageMinimalViewModel(Package package)
        {
            Id = package.Id;
            Title = package.Title;
            Description = package.Description;
            ImageUrl = package.AbsoluteImageUrl;
            CoverUrl = package.AbsoluteCoverUrl;
            CategoryId = package.CategoryId;
        }
    }
}