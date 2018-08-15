namespace Matlab.DataModel
{
    public class ImageSuggest
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string ImageUrl { get; set; }
        public virtual Article Article { get; set; }

    }
}